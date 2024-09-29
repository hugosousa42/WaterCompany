using WaterCompany.Data;
using WaterCompany.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WaterCompany.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly IClientRepository _clientRepository;

        public BillsController(IBillRepository billRepository, IClientRepository clientRepository)
        {
            _billRepository = billRepository;
            _clientRepository = clientRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _billRepository.GetBillAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _billRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public IActionResult AddClient()
        {
            var model = new AddItemViewModel
            {
                Volume = 1,
                Clients = _clientRepository.GetComboClients()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddClient(AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _billRepository.AddItemToBillAsync(model, this.User.Identity.Name);
                return RedirectToAction("Create");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _billRepository.DeleteDetailTempAsync(id.Value);
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _billRepository.ModifyBillDetailTempVolumeAsync(id.Value, 1);
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _billRepository.ModifyBillDetailTempVolumeAsync(id.Value, -1);
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> ConfirmBill()
        {
            var response = await _billRepository.ConfirmBillAsync(this.User.Identity.Name);
            if (response)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }
    }
}
