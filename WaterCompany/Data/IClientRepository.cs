using System.Collections.Generic;
using System.Linq;
using WaterCompany.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace WaterCompany.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {     
        Task<Client> GetClientByUserAsync(string userId);

        public IQueryable GetAllWithUsers();
        
        IEnumerable<SelectListItem> GetComboClients();
    }
}
