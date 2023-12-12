using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Usuarios;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _repositorioUsuario;


        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _repositorioUsuario = usuarioRepository;
        }


        public IActionResult Index()
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }

                List<Usuario> ListadoUsuarios = _repositorioUsuario.List();
                ListarUsuariosViewModel listarUsuariosVM = new ListarUsuariosViewModel(ListadoUsuarios);
                HerramientasUsuariosViewModel herramientasVM = new HerramientasUsuariosViewModel(listarUsuariosVM, HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("NombreDeUsuario"), HttpContext.Session.GetString("Rol"));

                if (ListadoUsuarios != null)
                {
                    return View(herramientasVM);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }


        [HttpGet]
        public IActionResult CrearUsuario()
        {   
            try
            {
                return View(new HerramientasUsuariosViewModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }

    
        [HttpPost]
        public IActionResult CrearUsuario(CrearUsuarioViewModel crearUsuarioVM)
        {   
            try
            {
                if(!ModelState.IsValid) return RedirectToAction("CrearUsuario");

                Usuario usuario = new Usuario(crearUsuarioVM);
                _repositorioUsuario.Create(usuario);
                
                return RedirectToAction("Index");                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }
    

        [HttpGet]
        public IActionResult EditarUsuario(int idUsuario)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToAction("Index");
                }

                ModificarUsuarioViewModel modificarUsuarioVM = new ModificarUsuarioViewModel(_repositorioUsuario.GetById(idUsuario));
                HerramientasUsuariosViewModel herramientasVM = new HerramientasUsuariosViewModel(modificarUsuarioVM, HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("NombreDeUsuario"), HttpContext.Session.GetString("Rol"));

                return View(herramientasVM);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }


        [HttpPost]
        public IActionResult EditarUsuario(ModificarUsuarioViewModel modificarUsuarioVM)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para editar un usuario";
                    return RedirectToAction("Index");
                }
                if(!ModelState.IsValid) return RedirectToAction("EditarUsuario");


                Usuario usuario2 = new Usuario(modificarUsuarioVM);
                _repositorioUsuario.Update(usuario2.Id, usuario2);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }

        
        public IActionResult DeleteUsuario(int idUsuario)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para eliminar un usuario";
                    return RedirectToAction("Index");
                }

                _repositorioUsuario.Remove(idUsuario);

                return RedirectToAction("Index");                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }


        private bool isLogged()
        {
            if (HttpContext.Session.GetString("Id") != null) 
                return true;
                
            return false;
        }


        private bool isAdmin()
        {
            if (HttpContext.Session.GetString("Rol") == "Administrador") 
                return true;
                
            return false;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}