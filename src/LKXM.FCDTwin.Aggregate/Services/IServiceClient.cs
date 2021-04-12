using LKXM.FCDTwin.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Aggregate.Services
{
    public interface IServiceClient
    {
        Task<List<TTest>> GetTest();
    }
}
