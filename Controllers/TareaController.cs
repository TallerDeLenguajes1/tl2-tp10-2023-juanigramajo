using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Tareas;

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
            if (!isAdmin())
            {
                var idUser = HttpContext.Session.GetString("Id");
                List<Tarea> ListadoTareas = repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                ListarTareasViewModel ListarTareasVM = new ListarTareasViewModel(ListadoTareas);

                if (ListadoTareas != null)
                {
                    return View(ListarTareasVM);
                }
                else
                {
                    return BadRequest();
                }
            }
            else 
            {
                List<Tarea> ListadoTareas = repositorioTarea.List();
                ListarTareasViewModel ListarTareasVM = new ListarTareasViewModel(ListadoTareas);

                if (ListadoTareas != null)
                {
                    return View(ListarTareasVM);
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

            return View(new CrearTareaViewModel());
        }

    
        [HttpPost]
        public IActionResult CrearTarea(CrearTareaViewModel crearTareaVM)
        {   
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if(!ModelState.IsValid) return RedirectToAction("CrearTarea");

            Tarea tarea = new Tarea(crearTareaVM);

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
            if (!isAdmin())
            {
                var idUser = HttpContext.Session.GetString("Id");
                List<Tarea> listadoPermitido = repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == idTarea);
                
                if (tarea == null)
                {
                    TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                    return RedirectToAction("Index");
                }
            }
            
            ModificarTareaViewModel modificarTareaVM = new ModificarTareaViewModel(repositorioTarea.GetById(idTarea));
            
            return View(modificarTareaVM);
        }


        [HttpPost]
        public IActionResult EditarTarea(ModificarTareaViewModel modificarTareaVM)
        {
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!isAdmin())
            {
                var idUser = HttpContext.Session.GetString("Id");
                List<Tarea> listadoPermitido = repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == modificarTareaVM.Id);
                
                if (tarea == null)
                {
                    TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                    return RedirectToAction("Index");
                }
            }
            if(!ModelState.IsValid) return RedirectToAction("EditarTarea");

            Tarea tarea2 = new Tarea(modificarTareaVM);
            repositorioTarea.Update(tarea2.Id, tarea2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteTarea(int idTarea)
        {
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!isAdmin())
            {
                var idUser = HttpContext.Session.GetString("Id");
                List<Tarea> listadoPermitido = repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == idTarea);
                
                if (tarea == null)
                {
                    TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                    return RedirectToAction("Index");
                }
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