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
            return View(new Usuario());
        }

    
        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {   
            repositorioUsuario.Create(usuario);
            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarUsuario(int idUsuario, Usuario user)
        {
            return View(repositorioUsuario.GetById(idUsuario));
        }


        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            var usuario2 = repositorioUsuario.GetById(usuario.Id);
            usuario2.Id = usuario.Id;
            usuario2.NombreDeUsuario = usuario.NombreDeUsuario;
            
            repositorioUsuario.Update(usuario.Id, usuario2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteUsuario(int idUsuario)
        {
            repositorioUsuario.Remove(idUsuario);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}