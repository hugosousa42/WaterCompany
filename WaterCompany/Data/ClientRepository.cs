using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;
        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task AssociateUserToClientAsync(int userId, int clientId)
        {
            var user = await _context.Users.FindAsync(userId);
            var client = await _context.Clients.FindAsync(clientId);

            if (user != null && client != null)
            {
                client.user = user;
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Clients.Include(p => p.user);
        }

        public async Task<Client> GetClientByUserAsync(string userId)
        {
            return await _context.Clients
                .Include(c => c.user)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }


        public IEnumerable<SelectListItem> GetComboClients()
        {
            var list = _context.Clients.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a client)",
                Value = "0"
            });

            return list;
        }
    }
}
