using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterCompany.Data;
using WaterCompany.Data.Entities;
using WaterCompany.Models;
using Vereyon.Web;

namespace WaterCompany.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly IFlashMessage _flashMessage;
        private readonly ICountryRepository _countryRepository;

        public CountriesController(
            IFlashMessage flashMessage,
            ICountryRepository countryRepository)
        {
            _flashMessage = flashMessage;
            _countryRepository = countryRepository;
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _countryRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            try
            {
                var countryId = await _countryRepository.DeleteCityAsync(city);
                return this.RedirectToAction($"Details", new { id = countryId });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{city.Name} is probably being used!";
                    ViewBag.ErrorMessage = $"{city.Name} cannot be deleted as there are users using it.</br></br>" +
                    $"Try deleting all the users that are using it first, " +
                    $"and then try deleting it again.";
                }

                return View("Error");
            }
            
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _countryRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            if (this.ModelState.IsValid)
            {
                var countryId = await _countryRepository.UpdateCityAsync(city);
                if (countryId != 0)
                {
                    return this.RedirectToAction($"Details", new { id = countryId });
                }
            }

            return this.View(city);
        }

        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            var model = new CityViewModel { CountryId = country.id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _countryRepository.AddCityAsync(model);
                return RedirectToAction("Details", new { id = model.CountryId });
            }

            return this.View(model);
        }

        public IActionResult Index()
        {
            return View(_countryRepository.GetCountriesWithCities());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetCountryWithCitiesAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _countryRepository.CreateAsync(country);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _flashMessage.Danger("This country already exists!");
                }
            }
            return View(country);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                await _countryRepository.UpdateAsync(country);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            try
            {
                await _countryRepository.DeleteAsync(country);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{country.Name} is probably being used!";
                    ViewBag.ErrorMessage = $"{country.Name} cannot be deleted as there are users using it.</br></br>" +
                    $"Try deleting all the users that are using it first, " +
                    $"and then try deleting it again.";
                }

                return View("Error");
            }

           
        }



    }
}
