using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        Task<IQueryable<Bill>> GetBillAsync(string userName);

        Task<IQueryable<BillDetailTemp>> GetDetailTempsAsync(string userName);
    
    }
}
