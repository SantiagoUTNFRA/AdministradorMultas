using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdministradorMultas.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers");  // Vuelve a la lista de usuarios después de eliminar al usuario.
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);  // Agrega los errores al modelo si no se pudo eliminar al usuario.
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario no encontrado");  // Agrega un error al modelo si no se encontró al usuario.
            }
            return View("GetUsers", _userManager.Users);  // Si no se pudo eliminar al usuario, vuelve a la lista de usuarios mostrando los errores.
        }

    }
}