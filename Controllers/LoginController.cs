using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioRepository repositorioUsuario;
        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            repositorioUsuario = new UsuarioRepository();
        }


         public IActionResult Index()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            List<Usuario> ListadoUsuarios = repositorioUsuario.List();
            Usuario usuarioLoggeado = ListadoUsuarios.FirstOrDefault(user => user.NombreDeUsuario == usuario.NombreDeUsuario && user.Password == usuario.Password);

            if (usuarioLoggeado == null)
            {
                TempData["MensajeError"] = "Nombre de usuario o contraseña incorrectos.";
                return RedirectToAction("Index");
            } else
            {
                loggearUsuario(usuarioLoggeado);
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }
        }

        private void loggearUsuario(Usuario usuario)
        {
            HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
            // HttpContext.Session.SetString("Contraseña", usuario.Password);
            HttpContext.Session.SetString("Rol", usuario.RolDelUsuario.ToString());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}