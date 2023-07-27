using AdministradorMultas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdministradorMultas.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Usuarios()
        {
            return View();
        }

    }
}
