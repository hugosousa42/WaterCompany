using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;
using WaterCompany.Models;

namespace WaterCompany.Data
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        Task<IQueryable<Bill>> GetBillAsync(string userName);

        Task<IQueryable<BillDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToBillAsync(AddItemViewModel model, string userName);

        Task ModifyBillDetailTempVolumeAsync(int id, double volume);

        Task DeleteDetailTempAsync(int id);

    }
}
