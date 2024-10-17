﻿using System;
using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data;
using WaterCompany.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaterCompany.Data.Entities;
using WaterCompany.Helpers;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace WaterCompany.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;

        public BillsController(
            IBillRepository billRepository,
            IClientRepository clientRepository,
            IUserHelper userHelper)
        {
            _billRepository = billRepository;
            _clientRepository = clientRepository;
            _userHelper = userHelper;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Bill> model;

            if (User.IsInRole("Admin"))
            {
                model = await _billRepository.GetAllBillsAsync(); // Retorna todas as faturas
            }
            else
            {
                model = await _billRepository.GetBillAsync(this.User.Identity.Name); // Retorna as faturas do usuário
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmBill(string email)
        {
            var response = await _billRepository.ConfirmBillAsync(email);

            if (response)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> ConsumptionDetails(int id)
        {
            var detail = await _billRepository.GetBillDetailTempByIdAsync(id);

            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        public async Task<IActionResult> BillDetails(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);

        }


        public async Task<IActionResult> Create()
        {
            var model = await _billRepository.GetDetailTempsAsync(this.User.Identity.Name);

            if (User.IsInRole("Admin"))
            {
                var otherModel = await _billRepository.GetAllBillDetailTempsAsync();
                return View("CreateByEmployee", otherModel);
            }
            else
            {
                return View("CreateByClient", model);
            }
        }

        public IActionResult AddClient()
        {
            var model = new AddUserViewModel
            {
                Volume = 1,
                Users = _userHelper.GetComboUsers()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddClient(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _billRepository.AddItemToBillAsync(model);
                return RedirectToAction("Create");
            }

            return View(model);
        }

        public async Task<IActionResult> AddVolume()
        {
            var userName = this.User.Identity.Name; // Obter o nome do usuário

            // Agora, busque o usuário pelo nome de usuário
            var user = await _userHelper.GetUserByEmailAsync(userName);
            var model = new AddVolumeViewModel
            {
                UserId = user.Id,
                Volume = 1
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddVolume(AddVolumeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newModel = new AddUserViewModel
                {
                    UserId = model.UserId,
                    Volume = model.Volume
                };
                // Chama o repositório para adicionar o volume à fatura
                await _billRepository.AddItemToBillAsync(newModel);

                // Redireciona para a ação "Create" após a adição bem-sucedida
                return RedirectToAction("Create");

            }
            return View(model);
        }

        public async Task<IActionResult> DeleteBill(int id)
        {
            await _billRepository.DeleteBillAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _billRepository.DeleteDetailTempAsync(id.Value);

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Pay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _billRepository.GetBillByIdAsync(id.Value);
            if (bill == null)
            {
                return NotFound();
            }

            var model = new PaymentViewModel
            {
                id = bill.id,
                PaymentDate = DateTime.Today,
                PaymentMethod = bill.Method
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _billRepository.PayBill(model);
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> UpdateVolume(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var billDetailTemp = await _billRepository.GetBillDetailTempByIdAsync(id.Value);

            if (billDetailTemp == null)
            {
                return NotFound();
            }


            return View(billDetailTemp);
        }

        // POST: UpdateVolume
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVolume(int id, double volume)
        {
            // Chama o método para atualizar o volume
            await _billRepository.ModifyBillDetailTempVolumeAsync(id, volume);

            // Após salvar a atualização, redireciona para outra ação
            return RedirectToAction("Create");
        }

    }
}
