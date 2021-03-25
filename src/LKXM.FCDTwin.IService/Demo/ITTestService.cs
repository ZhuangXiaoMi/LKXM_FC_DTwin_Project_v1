using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Entity;

namespace LKXM.FCDTwin.IService
{
    public interface ITTestService : IBaseService<TTest>
    {
        ResultResDto Init();
    }
}
