using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MvcCoreCryptography.Models;
using MvcCoreCryptography.Repositories;

namespace MvcCoreCryptography.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register
            (string nombre, string email, string password, string imagen)
        {
            await this.repo.RegisterUsuarioAsync(nombre, email, password, imagen);
            ViewData["MENSAJE"] = "Usuario registrado correctamente";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login
            (string email, string password)
        {
            Usuario user = await this.repo.LoginUserAsync(email, password);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
                return View(user);
        }
    }
}
