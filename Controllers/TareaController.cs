using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Tareas;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class TareaController : Controller
    {
        private ITareaRepository _repositorioTarea;
        private readonly ILogger<TareaController> _logger;


        public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository)
        {
            _logger = logger;
            _repositorioTarea = tareaRepository;
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
                if (!isAdmin())
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> ListadoTareas = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
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
                    List<Tarea> ListadoTareas = _repositorioTarea.List();
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                return RedirectToRoute(new {controller = "Shared", action = "Error"});
            }
        }


        [HttpGet]
        public IActionResult CrearTarea()
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }

                return View(new CrearTareaViewModel());                
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
        public IActionResult CrearTarea(CrearTareaViewModel crearTareaVM)
        {   
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!ModelState.IsValid) return RedirectToAction("CrearTarea");

                Tarea tarea = new Tarea(crearTareaVM);

                // la consigna pedía asumir que el tablero es el mismo, por eso envío un 1
                _repositorioTarea.Create(1, tarea);

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
        public IActionResult EditarTarea(int idTarea)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!isAdmin())
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> listadoPermitido = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == idTarea);
                    
                    if (tarea == null)
                    {
                        TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                        return RedirectToAction("Index");
                    }
                }
                
                ModificarTareaViewModel modificarTareaVM = new ModificarTareaViewModel(_repositorioTarea.GetById(idTarea));
                
                return View(modificarTareaVM);                
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
        public IActionResult EditarTarea(ModificarTareaViewModel modificarTareaVM)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!isAdmin())
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> listadoPermitido = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == modificarTareaVM.Id);
                    
                    if (tarea == null)
                    {
                        TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                        return RedirectToAction("Index");
                    }
                }
                if(!ModelState.IsValid) return RedirectToAction("EditarTarea");

                Tarea tarea2 = new Tarea(modificarTareaVM);
                _repositorioTarea.Update(tarea2.Id, tarea2);

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

        
        public IActionResult DeleteTarea(int idTarea)
        {
            try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!isAdmin())
                {
                    var idUser = HttpContext.Session.GetString("Id");
                    List<Tarea> listadoPermitido = _repositorioTarea.ListByUser(Convert.ToInt32(idUser));
                    Tarea tarea = listadoPermitido.FirstOrDefault(tarea => tarea.Id == idTarea);
                    
                    if (tarea == null)
                    {
                        TempData["ErrorMessage"] = "No tienes permisos para editar esta tarea";
                        return RedirectToAction("Index");
                    }
                }
                
                _repositorioTarea.Remove(idTarea);

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