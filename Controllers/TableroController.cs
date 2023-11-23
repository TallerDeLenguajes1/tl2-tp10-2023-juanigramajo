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
            List<Tablero> ListadoTableros = repositorioTablero.List();

            if (ListadoTableros != null)
            {
                return View(ListadoTableros);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult CrearTablero()
        {   
            return View(new Tablero());
        }

    
        [HttpPost]
        public IActionResult CrearTablero(Tablero tablero)
        {   
            repositorioTablero.Create(tablero);
            return RedirectToAction("Index");
        }
    

        [HttpGet]
        public IActionResult EditarTablero(int idTablero)
        {
            return View(repositorioTablero.GetById(idTablero));
        }


        [HttpPost]
        public IActionResult EditarTablero(Tablero tablero)
        {
            var tablero2 = repositorioTablero.GetById(tablero.Id);
            tablero2.Nombre = tablero.Nombre;
            tablero2.Descripcion = tablero.Descripcion;
            
            repositorioTablero.Update(tablero.Id, tablero2);

            return RedirectToAction("Index");
        }

        
        public IActionResult DeleteTablero(int idTablero)
        {
            repositorioTablero.Remove(idTablero);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}