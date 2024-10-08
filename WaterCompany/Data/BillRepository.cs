﻿using System;
using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;
using WaterCompany.Helpers;
using Microsoft.EntityFrameworkCore;
using WaterCompany.Models;
using WaterCompany.Migrations;

namespace WaterCompany.Data
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public BillRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task<IQueryable<BillDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.BillDetailsTemp
                .Include(c => c.Client)
                .Where(o => o.User == user)
                .OrderBy(o => o.Client.Name);
        }

        public async Task<IQueryable<Bill>> GetBillAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsyc(user, "Admin"))
            {
                return _context.Bills
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Client)
                    .OrderByDescending(o => o.DateOfIssue);
            }

            return _context.Bills
                .Include(o => o.Items)
                .ThenInclude(p => p.Client)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.DateOfIssue);
        }

        public async Task AddItemToBillAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            var client = await _context.Clients.FindAsync(model.ClienttId);
            if (client == null)
            {
                return;
            }

            var billDetailTemp = await _context.BillDetailsTemp
                .Where(bdt => bdt.User == user && bdt.Client == client)
                .FirstOrDefaultAsync();

            if (billDetailTemp == null)
            {
                billDetailTemp = new BillDetailTemp
                {
                    Client = client,
                    Volume = model.Volume,
                    User = user,
                };

                _context.BillDetailsTemp.Add(billDetailTemp);
            }
            else
            {
                billDetailTemp.Volume += model.Volume;
                _context.BillDetailsTemp.Update(billDetailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ModifyBillDetailTempVolumeAsync(int id, double volume)
        {
            var billDetailTemp = await _context.BillDetailsTemp.FindAsync(id);
            if (billDetailTemp == null)
            {
                return;
            }

            billDetailTemp.Volume += volume;
            if (billDetailTemp.Volume > 0)
            {
                _context.BillDetailsTemp.Update(billDetailTemp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var billDetailTemp = await _context.BillDetailsTemp.FindAsync(id);
            if (billDetailTemp == null)
            {
                return;
            }

            _context.BillDetailsTemp.Remove(billDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmBillAsync(string usarName)
        {
            var user = await _userHelper.GetUserByEmailAsync(usarName);
            if (user == null)
            {
                return false;
            }

            var billTmps = await _context.BillDetailsTemp
                .Include(o => o.Client)
                .Where(o => o.User == user)
                .ToListAsync();

            if (billTmps == null || billTmps.Count == 0)
            {
                return false;
            }

            var detail = billTmps.Select(o => new BillDetail
            {
                Client = o.Client,
                Volume = o.Volume     
            }).ToList();

            var bill = new Bill
            {
                DateOfIssue = DateTime.UtcNow,
                User = user,
                Items = detail
            };
            await CreateAsync(bill);
            _context.BillDetailsTemp.RemoveRange(billTmps);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task PayBill(PaymentViewModel model)
        {
            var bill = await _context.Bills.FindAsync(model.id);
            if (bill == null)
            {
                return;
            }

            bill.PaymentDate = model.PaymentDate;
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }

        public async Task<Bill> GetBillAsync(int id)
        {
            return await _context.Bills.FindAsync(id);
        }
    }
}
