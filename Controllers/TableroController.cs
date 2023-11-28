using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Tableros;

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
            if (!isLogged())
            {
                TempData["ErrorMessage"] = "Debes iniciar sesión para acceder a esta sección.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            string rolUsuario = HttpContext.Session.GetString("Rol");

            if(!isAdmin())
            {
                var idUser = HttpContext.Session.GetString("Id");
                List<Tablero> ListadoTableros = repositorioTablero.ListByUser(Convert.ToInt32(idUser));

                ListarTablerosViewModel CrearTableroVM = new ListarTablerosViewModel(ListadoTableros);

                if (ListadoTableros != null)
                {
                    ViewBag.Rol = rolUsuario;
                    return View(CrearTableroVM);
                }
                else
                {
                    return BadRequest();
                }
            } 
            else 
            {
                List<Tablero> ListadoTableros = repositorioTablero.List();
                ListarTablerosViewModel listarTablerosVM = new ListarTablerosViewModel(ListadoTableros);

                if (ListadoTableros != null)
                {
                    ViewBag.Rol = rolUsuario;
                    return View(listarTablerosVM);
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

            return View(new CrearTableroViewModel());
        }

    
        [HttpPost]
        public IActionResult CrearTablero(CrearTableroViewModel crearTableroVM)
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
            repositorioTablero.Create(tablero);

            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarTablero(int idTablero)
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

            ModificarTableroViewModel modificarTableroVM = new ModificarTableroViewModel(repositorioTablero.GetById(idTablero));

            return View(modificarTableroVM);
        }


        [HttpPost]
        public IActionResult EditarTablero(ModificarTableroViewModel modificarTableroVM)
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
            repositorioTablero.Update(tablero2.Id, tablero2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteTablero(int idTablero)
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