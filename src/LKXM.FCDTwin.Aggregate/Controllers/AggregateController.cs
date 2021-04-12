using LKXM.FCDTwin.Aggregate.Services;
using LKXM.FCDTwin.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Aggregate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AggregateController : Controller
    {
        private readonly IServiceClient _serviceClient;

        public AggregateController(IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<ActionResult<List<TTest>>> Get()
        {
            List<TTest> tests = await _serviceClient.GetTest();
            return tests;
        }
    }
}
