using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Infrastructure;
using LKXM.FCDTwin.IService;
using Microsoft.AspNetCore.Mvc;

namespace LKXM.FCDTwin.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiGroup(GroupNameEnum.Test)]
    public class TTestController : BaseController
    {
        private readonly ITTestService _tTestService;

        public TTestController(ITTestService tTestService)
        {
            _tTestService = tTestService;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/t_test/init")]
        public ResultResDto Init()
            => _tTestService.Init();

    }
}
