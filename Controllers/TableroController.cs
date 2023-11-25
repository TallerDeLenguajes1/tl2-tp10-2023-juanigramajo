using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class TableroController : Controller
    {
        private ITableroRepository repositorioTablero;
        private readonly ILogger<TableroController> _logger;


        public TableroController(ILogger<TableroController> logger)
        {
            _logger = logger;
            repositorioTablero = new TableroRepository();
        }


        public IActionResult Index()
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});

            if(isAdmin())
            {
                List<Tablero> ListadoTableros = repositorioTablero.List();

                if (ListadoTableros != null)
                {
                    return View(ListadoTableros);
                }
                else
                {
                    return BadRequest();
                }
            } else 
            {
                var idUser = HttpContext.Session.GetString("Id");
                var ListadoTableros = repositorioTablero.ListByUser(Convert.ToInt32(idUser));

                if (ListadoTableros != null)
                {
                    return View(ListadoTableros);
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpGet]
        public IActionResult CrearTablero()
        {   
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            return View(new Tablero());
        }

    
        [HttpPost]
        public IActionResult CrearTablero(Tablero tablero)
        {   
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid) return RedirectToAction("CrearTablero");
            repositorioTablero.Create(tablero);
            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarTablero(int idTablero)
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            return View(repositorioTablero.GetById(idTablero));
        }


        [HttpPost]
        public IActionResult EditarTablero(Tablero tablero)
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid) return RedirectToAction("EditarTablero");

            var tablero2 = repositorioTablero.GetById(tablero.Id);
            tablero2.Nombre = tablero.Nombre;
            tablero2.Descripcion = tablero.Descripcion;
            
            repositorioTablero.Update(tablero.Id, tablero2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteTablero(int idTablero)
        {
            if(!isLogged()) return RedirectToRoute(new { controller = "Login", action = "Index"});
            if(!isAdmin()) return RedirectToAction("Index");
            
            repositorioTablero.Remove(idTablero);

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