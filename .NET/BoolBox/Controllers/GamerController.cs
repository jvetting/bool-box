using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoolBox.Controllers
{
    public class GamerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
