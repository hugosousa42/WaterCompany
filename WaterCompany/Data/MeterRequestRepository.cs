using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public class MeterRequestRepository : GenericRepository<MeterRequest>, IMeterRequestRepository
    {
        private readonly DataContext _context;

        public MeterRequestRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MeterRequest>> GetAllNotDeliveredAsync()
        {
            return await _context.MeterRequests
                .Include(m => m.City)
                .Where(m => m.IsDelivered == false && m.IsProcessed == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<MeterRequest>> GetAllUnprocessedAsync()
        {
           return await _context.MeterRequests
                .Include(m => m.City)
                .Where(m => m.IsProcessed == false)
                .ToListAsync();
        }

        public async Task MarkAsDeliveredAsync(int id)
        {
            var meterRequest = await GetByIdAsync(id);
            if (meterRequest != null)
            {
                meterRequest.IsDelivered = true;
                _context.MeterRequests.Update(meterRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsProcessedAsync(int id)
        {
            var meterRequest = await GetByIdAsync(id);
            if (meterRequest != null)
            {
                meterRequest.IsProcessed = true;
                _context.MeterRequests.Update(meterRequest);
                await _context.SaveChangesAsync();
            }

        }
    }
}
