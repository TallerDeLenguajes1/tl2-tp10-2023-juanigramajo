using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Tableros;
using tl2_tp10_2023_juanigramajo.ViewModels.Usuarios;

namespace tl2_tp10_2023_juanigramajo.Controllers
{
    public class TableroController : Controller
    {
        private ITableroRepository _repositorioTablero;
        private readonly ILogger<TableroController> _logger;


        public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository)
        {
            _logger = logger;
            _repositorioTablero = tableroRepository;
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

                string rolUsuario = HttpContext.Session.GetString("Rol");
                string idUser = HttpContext.Session.GetString("Id");

                if(!isAdmin())
                {
                    List<Tablero> ListadoTableros = _repositorioTablero.RestListByUser(Convert.ToInt32(idUser));
                    List<Tablero> ListadoMisTableros = _repositorioTablero.ListByUser(Convert.ToInt32(idUser));
                    ListarTablerosViewModel listarTablerosVM = new ListarTablerosViewModel(ListadoTableros, ListadoMisTableros, idUser);

                    if (ListadoTableros != null)
                    {
                        return View(listarTablerosVM);
                    }
                    else
                    {
                        return BadRequest();
                    }
                } 
                else 
                {
                    List<Tablero> ListadoTableros = _repositorioTablero.RestListByUser(Convert.ToInt32(idUser));
                    List<Tablero> ListadoMisTableros = _repositorioTablero.ListByUser(Convert.ToInt32(idUser));
                    ListarTablerosViewModel listarTablerosVM = new ListarTablerosViewModel(ListadoTableros, ListadoMisTableros, idUser);

                    if (ListadoTableros != null)
                    {
                        return View(listarTablerosVM);
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
        public IActionResult CrearTablero()
        {   try
            {
                if (!isLogged())
                {
                    TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if(!isAdmin())
                {   
                    TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                    return RedirectToAction("Index");
                }

                return View(new CrearTableroViewModel(Convert.ToInt32(HttpContext.Session.GetString("Id"))));                
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
        public IActionResult CrearTablero(CrearTableroViewModel crearTableroVM)
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
                    TempData["ErrorMessage"] = "No tienes permisos para crear un tablero";
                    return RedirectToAction("Index");
                }
                if(!ModelState.IsValid) return RedirectToAction("CrearTablero");


                Tablero tablero = new Tablero(crearTableroVM);
                _repositorioTablero.Create(crearTableroVM.IdUsuarioPropietario, tablero);

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
        public IActionResult EditarTablero(int idTablero)
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
                    TempData["ErrorMessage"] = "No tienes permisos para editar un tablero";
                    return RedirectToAction("Index");
                }

                ModificarTableroViewModel modificarTableroVM = new ModificarTableroViewModel(_repositorioTablero.GetById(idTablero));

                return View(modificarTableroVM);                
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
        public IActionResult EditarTablero(ModificarTableroViewModel modificarTableroVM)
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
                    TempData["ErrorMessage"] = "No tienes permisos para editar un tablero";
                    return RedirectToAction("Index");
                }
                if(!ModelState.IsValid) return RedirectToAction("EditarTablero");

                Tablero tablero2 = new Tablero(modificarTableroVM);
                _repositorioTablero.Update(tablero2.Id, tablero2);

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

        
        public IActionResult DeleteTablero(int idTablero)
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
                    TempData["ErrorMessage"] = "No tienes permisos para eliminar un tablero";
                    return RedirectToAction("Index");
                }
                
                _repositorioTablero.Remove(idTablero);

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