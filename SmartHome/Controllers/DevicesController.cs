using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Controllers
{
    public class DevicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
