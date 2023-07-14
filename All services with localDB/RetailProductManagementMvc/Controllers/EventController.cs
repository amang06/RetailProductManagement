using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailProductManagementMvc.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index(string message)
        {
            return View((object)message);
        }
        public IActionResult Error(string message)
        {
            return View((object)message);
        }
    }
}
