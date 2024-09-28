using WaterCompany.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaterCompany.Models;

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
    }
}
