using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace UnitTestsCourse.Mocking
{
    public class HousekeeperRepository : IHousekeeperRepository
    {
        public IQueryable<Housekeeper> GetHousekeepers()
        {
            return new List<Housekeeper>().AsQueryable();
        }
    }

    public interface IHousekeeperRepository
    {
        IQueryable<Housekeeper> GetHousekeepers();
    }
}