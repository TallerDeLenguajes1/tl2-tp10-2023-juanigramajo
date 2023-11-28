using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class TareaController : Controller
    {
        private ITareaRepository repositorioTarea;
        private readonly ILogger<TareaController> _logger;


        public TareaController(ILogger<TareaController> logger)
        {
            _logger = logger;
            repositorioTarea = new TareaRepository();
        }


        public IActionResult Index()
        {
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            
            if (isAdmin())
            {
                List<Tarea> ListadoTareas = repositorioTarea.List();

                if (ListadoTareas != null)
                {
                    return View(ListadoTareas);
                }
                else
                {
                    return BadRequest();
                }
            }
            else 
            {
                var idUser = HttpContext.Session.GetInt32("Id");
                List<Tarea> ListadoTareas = repositorioTarea.ListByUser((int)idUser);

                if (ListadoTareas != null)
                {
                    return View(ListadoTareas);
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpGet]
        public IActionResult CrearTarea()
        {   
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            return View(new Tarea());
        }

    
        [HttpPost]
        public IActionResult CrearTarea(Tarea tarea)
        {   
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if(!ModelState.IsValid) return RedirectToAction("CrearTarea");

            // la consigna pedía asumir que el tablero es el mismo, por eso envío un 1
            repositorioTarea.Create(1, tarea);
            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarTarea(int idTarea)
        {
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            
            return View(repositorioTarea.GetById(idTarea));
        }


        [HttpPost]
        public IActionResult EditarTarea(Tarea tarea)
        {
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if(!ModelState.IsValid) return RedirectToAction("EditarTarea");

            var tarea2 = repositorioTarea.GetById(tarea.Id);

            tarea2.Nombre = tarea.Nombre;
            tarea2.Descripcion = tarea.Descripcion;
            tarea2.Color = tarea.Color;
            tarea2.Estado = tarea.Estado;
            
            repositorioTarea.Update(tarea.Id, tarea2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteTarea(int idTarea)
        {
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            repositorioTarea.Remove(idTarea);

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