using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels;
using tl2_tp10_2023_juanigramajo.ViewModels.Usuarios;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioRepository repositorioUsuario;
        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            repositorioUsuario = usuarioRepository;
        }


         public IActionResult Index()
        {
            return View(new HerramientasUsuariosViewModel());
        }


        [HttpPost]
        public IActionResult Login(HerramientasUsuariosViewModel HerramientaUsuarioVM)
        {
            List<Usuario> ListadoUsuarios = repositorioUsuario.List();
            Usuario usuarioLoggeado = ListadoUsuarios.FirstOrDefault(user => user.NombreDeUsuario == HerramientaUsuarioVM.LoginVM.NombreDeUsuario && user.Password == HerramientaUsuarioVM.LoginVM.Password);

            if (usuarioLoggeado == null)
            {
                TempData["MensajeError"] = "Nombre de usuario o contraseña incorrectos.";
                _logger.LogWarning($"Intento de acceso invalido - Usuario: [ {HerramientaUsuarioVM.LoginVM.NombreDeUsuario} ] Clave ingresada: [ {HerramientaUsuarioVM.LoginVM.Password} ]");
                return RedirectToAction("Index");
            } else
            {
                HerramientasUsuariosViewModel herramientasUsuarioVM = loggearUsuario(usuarioLoggeado);
                _logger.LogInformation($"El usuario [ {HerramientaUsuarioVM.LoginVM.NombreDeUsuario} ] ingresó corecctamente.");
                return View("~/Views/Home/Index.cshtml", herramientasUsuarioVM);
            }
        }

        private HerramientasUsuariosViewModel loggearUsuario(Usuario usuario)
        {
            HttpContext.Session.SetString("Id", usuario.Id.ToString());
            HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
            HttpContext.Session.SetString("Rol", usuario.RolDelUsuario.ToString());

            HerramientasUsuariosViewModel herramientasVM = new HerramientasUsuariosViewModel(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("NombreDeUsuario"), HttpContext.Session.GetString("Rol"));
            return (herramientasVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}