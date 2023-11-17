using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class UsuarioController : Controller
    {
        private static List<Usuario> ListUsuarios = new List<Usuario>();
        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View(ListUsuarios);
        }


        [HttpGet]
        public IActionResult CrearUsuario()
        {   
            return View(new Usuario());
        }

    
        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {   
            usuario.Id = ListUsuarios.Count()+1;
            ListUsuarios.Add(usuario);
            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarUsuario(int idUsuario)
        {  
            return View(ListUsuarios.FirstOrDefault(usuario => usuario.Id == idUsuario));
        }


        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {   
            
            var usuario2 = ListUsuarios.FirstOrDefault( producto => producto.Id == producto.Id);
            usuario2.Id = usuario.Id;
            usuario2.NombreDeUsuario = usuario.NombreDeUsuario;

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteUsuario(int idUsuario)
        {  
        var usuarioBuscado = ListUsuarios.FirstOrDefault( usuario => usuario.Id == idUsuario);
        ListUsuarios.Remove(usuarioBuscado);
        return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}