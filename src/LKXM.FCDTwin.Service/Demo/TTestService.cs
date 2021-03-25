using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Entity;
using LKXM.FCDTwin.IRepository;
using LKXM.FCDTwin.IService;

namespace LKXM.FCDTwin.Service
{
    public class TTestService : BaseService<TTest>, ITTestService
    {
        public TTestService(ITTestRepository tTestRepository)
            : base(tTestRepository)
        {

        }

        public ResultResDto Init()
        {
            var result = new ResultResDto();
            var entity = new TTest
            {
                name = "张三",
                age = 18
            };
            _baseRepository.Add(entity);
            return result;
        }
    }
}
