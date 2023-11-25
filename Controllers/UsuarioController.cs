using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository repositorioUsuario;
        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            repositorioUsuario = new UsuarioRepository();
        }


        public IActionResult Index()
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            List<Usuario> ListadoUsuarios = repositorioUsuario.List();

            if (ListadoUsuarios != null)
            {
                return View(ListadoUsuarios);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult CrearUsuario()
        {   
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            return View(new Usuario());
        }

    
        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {   
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid) return RedirectToAction("CrearUsuario");
            repositorioUsuario.Create(usuario);
            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarUsuario(int idUsuario)
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            return View(repositorioUsuario.GetById(idUsuario));
        }


        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid) return RedirectToAction("EditarUsuario");
            var usuario2 = repositorioUsuario.GetById(usuario.Id);
            usuario2.NombreDeUsuario = usuario.NombreDeUsuario;
            
            repositorioUsuario.Update(usuario.Id, usuario2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteUsuario(int idUsuario)
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            repositorioUsuario.Remove(idUsuario);

            return RedirectToAction("Index");
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