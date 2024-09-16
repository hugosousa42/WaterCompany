using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterCompany.Data;
using WaterCompany.Data.Entities;
using WaterCompany.Helpers;
using WaterCompany.Models;

namespace WaterCompany.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;
        public ClientsController(IClientRepository ClientRepository,
            IUserHelper userHelper)
        {
            _clientRepository = ClientRepository;
            _userHelper = userHelper;
        }

        // GET: Clients
        public IActionResult Index()
        {
            return View(_clientRepository.GetAll());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {

                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\clients",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/clients/{file}";
                }
                var client = this.ToClient(model, path);

                //TODO: Change to the user that is here
                client.user = await _userHelper.GetUserByEmailAsync("hugosb9@gmail.com");
                await _clientRepository.CreateAsync(client);
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        private Client ToClient(ClientViewModel model, string path)
        {
            return new Client
            {
                id = model.id,
                Name = model.Name,
                ImageUrl = path,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                RegistrationDate = model.RegistrationDate,
                user = model.user        
            };
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            var model = this.ToClientViewModel(client);
            return View(model);
        }

        private ClientViewModel ToClientViewModel(Client client)
        {
            return new ClientViewModel
            {
                id = client.id,
                Name = client.Name,
                ImageUrl = client.ImageUrl,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                RegistrationDate = client.RegistrationDate,
                user = client.user
            };
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( ClientViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\clients",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/clients/{file}";
                    }

                    var client = this.ToClient(model, path);

                    //TODO: Change to the user that is here
                    client.user = await _userHelper.GetUserByEmailAsync("hugosb9@gmail.com");
                    await _clientRepository.UpdateAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _clientRepository.ExistsAsync(model.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            await _clientRepository.DeleteAsync(client);
            return RedirectToAction(nameof(Index));
        }

    }
}
