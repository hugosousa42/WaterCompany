using WaterCompany.Data.Entities;

namespace WaterCompany.Data
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base(context)
        {

        }
    }
}
