using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MvcCoreCryptography.Helpers;
using MvcCoreCryptography.Models;
using MvcCoreCryptography.Repositories;

namespace MvcCoreCryptography.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;
        private HelperPathProvider pathProvider;
        private HelperMails helperMails;

        public UsuariosController
            (RepositoryUsuarios repo,
            HelperPathProvider pathProvider,
            HelperMails helperMails)
        {
            this.repo = repo;
            this.pathProvider = pathProvider;
            this.helperMails = helperMails;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register
            (string nombre, string email, string password, string imagen)
        {
            Usuario user = await this.repo.RegisterUsuarioAsync(nombre, email, password, imagen);
            string serverUrl = pathProvider.MapUrlPath();
            serverUrl = serverUrl + "/Usuarios/ActivateUser/?token=" + user.TokenMail;
            string mensaje = "<h3>Usuario registrado</h3>";
            mensaje += "<p>Debe activar su cuenta pulsando el siguiente enlace</p>";
            mensaje += "<p>" + serverUrl + "</p>";
            mensaje += "<a href='" + serverUrl + "'>" + serverUrl + "</a>";
            await helperMails.SendMailAsync(user.Email, "Registro Usuario", mensaje);
            ViewData["MENSAJE"] = "Usuario registrado correctamente. Activa el usuario con el mail que te hemos enviado";
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

        public async Task<IActionResult> ActivateUser(string token)
        {
            await this.repo.ActivateUserAsync(token);
            ViewData["MENSAJE"] = "Cuenta activada correctamente";
            return View();
        }
    }
}
