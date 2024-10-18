using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;
using WaterCompany.Models;

namespace WaterCompany.Data
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        Task<IQueryable<Bill>> GetAllBillsAsync();

        Task<IQueryable<Bill>> GetBillAsync(string userName);

        Task<IQueryable<BillDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToBillAsync(AddUserViewModel model);

        Task<IQueryable<BillDetailTemp>> GetAllBillDetailTempsAsync();

        Task<bool> ConfirmBillAsync(string usarName);

        Task PayBill(PaymentViewModel model);

        Task<Bill> GetBillByIdAsync(int id);

        Task DeleteDetailTempAsync(int id);

        Task ModifyBillDetailTempVolumeAsync(int id, double newVolume);

        Task<BillDetailTemp> GetBillDetailTempByIdAsync(int id);

        Task<Bill> GetBillAsync(int id);

        Bill GetLastBillToPay(string userId);

        Task DeleteBillAsync(int id);

        Task DeleteBillItemsAsync(int id);

    }
}
