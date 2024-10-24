﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterCompany.Data;
using WaterCompany.Helpers;
using WaterCompany.Models;

namespace WaterCompany.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        public ClientsController(IClientRepository ClientRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
             IConverterHelper converterHelper)
        {
            _clientRepository = ClientRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;

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
                return new NotFoundViewResult("Error404");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("Error404");
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
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "clients");
                }

                var client = _converterHelper.ToClient(model, path, true);

                //TODO: Change to the user that is here
                client.user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _clientRepository.CreateAsync(client);
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }


        // GET: Clients/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("Error404");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("Error404");
            }

            var model = _converterHelper.ToClientViewModel(client);
            return View(model);
        }


        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Clients");
                    }

                    var client = _converterHelper.ToClient(model, path, false);

                    //TODO: Change to the user that is here
                    client.user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
                return new NotFoundViewResult("Error404");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("Error404");
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            try
            {
                await _clientRepository.DeleteAsync(client);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{client.Name} is probably being used!";
                    ViewBag.ErrorMessage = $"{client.Name} cannot be deleted as there are bills using it.</br></br>" +
                    $"Try deleting all the bills that are using it first, " +
                    $"and then try deleting it again.";
                }

                return View("Error");
            }

        }

        public IActionResult ClientNotFound()
        {
            return View();
        }

    }
}
