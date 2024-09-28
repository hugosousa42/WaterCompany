using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;
using WaterCompany.Helpers;
using Microsoft.EntityFrameworkCore;

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
    }
}
