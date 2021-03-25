using LKXM.FCDTwin.Entity;
using LKXM.FCDTwin.IRepository;

namespace LKXM.FCDTwin.Repository.PostgreSQL.Demo
{
    public class TTestRepository : BaseRepository<TTest>, ITTestRepository
    {
        public TTestRepository(ApiDbContext dbContext) : base(dbContext)
        {
        }
    }
}
