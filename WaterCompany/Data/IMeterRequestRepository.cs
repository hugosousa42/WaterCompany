using System.Collections.Generic;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public interface IMeterRequestRepository : IGenericRepository<MeterRequest>
    {
        Task<IEnumerable<MeterRequest>> GetAllUnprocessedAsync();

        Task<IEnumerable<MeterRequest>> GetAllNotDeliveredAsync();

        Task MarkAsProcessedAsync(int id);

        Task MarkAsDeliveredAsync(int id);
    }
}
