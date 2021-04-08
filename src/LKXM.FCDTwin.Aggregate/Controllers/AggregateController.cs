using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Aggregate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AggregateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
