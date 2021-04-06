using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LKXM.FCDTwin.Api.Controllers
{
    /// <summary>
    /// Consul 心跳检测地址
    /// </summary>
    //[Route("api/[controller]/[action]")]
    [ApiController]
    [ApiGroup(GroupNameEnum.Test)]
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            Console.WriteLine("进行心跳检测");
            return Ok("连接正常");
        }
    }
}
