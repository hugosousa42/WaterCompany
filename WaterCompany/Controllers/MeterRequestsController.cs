using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterCompany.Data;
using WaterCompany.Data.Entities;
using WaterCompany.Migrations;
using WaterCompany.Models;

namespace WaterCompany.Controllers
{
    public class MeterRequestsController : Controller
    {
        private readonly IMeterRequestRepository _meterRequestRepository;
        private readonly ICountryRepository _countryRepository;

        public MeterRequestsController(
            IMeterRequestRepository meterRequestRepository,
            ICountryRepository countryRepository)
        {
            _meterRequestRepository = meterRequestRepository;
            _countryRepository = countryRepository;
        }
        public IActionResult Index()
        {
            var model = new MeterRequestViewModel
            {          
                Countries = _countryRepository.GetComboCountries(),
                Cities = _countryRepository.GetComboCities(0)
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Index(MeterRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var city = await _countryRepository.GetCityAsync(model.CityId);

                var meterRequest = new Data.Entities.MeterRequest
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,                 
                    Address = model.Address,
                    PhoneNumber=model.PhoneNumber,
                    CityId = city.id,
                    City = city,
               };

            await _meterRequestRepository.CreateAsync(meterRequest);
               
            }


            return View(model);
        }
        
        
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> EmployeeMeterRequest()
        {
            var meterRequests = await _meterRequestRepository.GetAllUnprocessedAsync();
            return View(meterRequests);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> RequestToAdmin(int id)
        {
            var meterRequest = await _meterRequestRepository.GetByIdAsync(id);

            if (meterRequest == null)
            {
                return NotFound(); 
            }

            await _meterRequestRepository.MarkAsProcessedAsync(meterRequest.id);

            return RedirectToAction("EmployeeMeterRequest");
        }

      
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminMeterRequest()
        {
            var meterRequests = await _meterRequestRepository.GetAllNotDeliveredAsync();
            return View(meterRequests);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeliverMeter(int id)
        {
            var meterRequest = await _meterRequestRepository.GetByIdAsync(id);

            if (meterRequest == null)
            {
                return NotFound();
            }

            await _meterRequestRepository.MarkAsDeliveredAsync(meterRequest.id);

            return RedirectToAction("AdminMeterRequest");
        }


        [HttpPost]
        [Route("MeterRequests/GetCitiesAsync")]
        public async Task<JsonResult> GetCitiesAsync(int countryId)
        {
            var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
            return Json(country.Cities.OrderBy(c => c.Name));
        }
    }
}
