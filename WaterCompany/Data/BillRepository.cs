using System;
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
            // Busca o usuário baseado no nome de usuário fornecido
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return Enumerable.Empty<BillDetailTemp>().AsQueryable(); // Retorna uma lista vazia se o usuário não for encontrado
            }

            // Retorna os detalhes temporários da conta (BillDetailTemp) associados ao usuário
            return _context.BillDetailsTemp
                .Include(bdt => bdt.User) // Inclui os detalhes do cliente se necessário
                .Where(bdt => bdt.User.Id == user.Id) // Filtra os detalhes temporários para o usuário específico
                .OrderBy(bdt => bdt.User.UserName); // Ordena pelo nome do cliente
        }

        public async Task<IQueryable<Bill>> GetBillAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return Enumerable.Empty<Bill>().AsQueryable(); 
            }

            return _context.Bills
            .Include(b => b.User) 
            .Include(b => b.Items) 
            .Where(b => b.User.Id == user.Id) 
            .OrderByDescending(b => b.DateOfIssue); 
        }

        public async Task AddItemToBillAsync(AddUserViewModel model)
        {         
            var user = await _userHelper.GetUserByIdAsync(model.UserId); 
            if (user == null)
            {
                return; 
            }
          
            var billDetailTemp = await _context.BillDetailsTemp
                .Where(bdt => bdt.User.Id == user.Id) 
                .FirstOrDefaultAsync();

            if (billDetailTemp == null)
            {              
                billDetailTemp = new BillDetailTemp
                {
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

        public async Task ModifyBillDetailTempVolumeAsync(int id, double newVolume)
        {
            var billDetailTemp = await _context.BillDetailsTemp
                                        .Include(b => b.User)
                                        .FirstOrDefaultAsync(b => b.id == id);
            if (billDetailTemp == null)
            {
                return;
            }


            billDetailTemp.Volume = newVolume;


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



        public async Task PayBill(PaymentViewModel model)
        {
            var bill = await _context.Bills.FindAsync(model.id);
            if (bill == null)
            {
                return;
            }

            bill.Method = model.PaymentMethod;
            bill.PaymentDate = model.PaymentDate;
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }

        public async Task<Bill> GetBillAsync(int id)
        {
            return await _context.Bills.FindAsync(id);
        }


        public async Task<IQueryable<Bill>> GetAllBillsAsync()
        {
            return _context.Bills
                .Include(b => b.User) 
                .Include(b => b.Items) 
                .OrderByDescending(b => b.DateOfIssue);
        }

        public async Task<IQueryable<BillDetailTemp>> GetAllBillDetailTempsAsync()
        {

            return _context.BillDetailsTemp
                .Include(bdt => bdt.User)
                .OrderBy(bdt => bdt.User.Id);
        }

        public async Task<bool> ConfirmBillAsync(string email)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false; 
            }
          
            var billTmps = await _context.BillDetailsTemp
                .Where(o => o.User == user) 
                .ToListAsync();

            if (billTmps == null || billTmps.Count == 0)
            {
                return false; 
            }

            var details = billTmps.Select(o => new BillDetail
            {
                User = user, 
                Volume = o.Volume,  
            }).ToList();

           
            var bill = new Bill
            {
                DateOfIssue = DateTime.UtcNow,
                User = user, 
                Items = details 
            };

        
            await CreateAsync(bill); 

           
            _context.BillDetailsTemp.RemoveRange(billTmps);
            await _context.SaveChangesAsync(); 

            return true; 
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _context.Bills
            .Include(b => b.User)
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.id == id);
        }

        public async Task<BillDetailTemp> GetBillDetailTempByIdAsync(int id)
        {
            return await _context.BillDetailsTemp
           .Include(b => b.User)
           .FirstOrDefaultAsync(b => b.id == id);
        }

        public Bill GetLastBillToPay(string userId)
        {
            return _context.Bills
         .Include(b => b.User)
         .Include(b => b.Items)
         .Where(b => b.Method == PaymentMethod.NotPaid && b.DateOfIssue != null && b.User.Id == userId) 
         .OrderByDescending(b => b.DateOfIssue)
         .FirstOrDefault();
        }

        public async Task DeleteBillAsync(int id)
        {
            var bill = await _context.Bills
           .Include(b => b.Items) // Certifique-se de incluir os detalhes da fatura
           .FirstOrDefaultAsync(b => b.id == id);

            if (bill == null)
            {
                return;
            }

            if (bill.Items != null && bill.Items.Any())
            {
                _context.BillDetails.RemoveRange(bill.Items);
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

        }
    }
    
}

