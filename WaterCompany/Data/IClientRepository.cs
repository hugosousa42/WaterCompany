using System.Collections.Generic;
using System.Linq;
using WaterCompany.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WaterCompany.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboClients();
    }
}
