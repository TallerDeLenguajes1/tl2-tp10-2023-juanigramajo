using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_juanigramajo.Models;
using tl2_tp10_2023_juanigramajo.ViewModels.Usuarios;

namespace tl2_tp10_2023_juanigramajo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!isLogged())
        {
            return View(new HerramientasUsuariosViewModel());
        }
        else 
        {
            HerramientasUsuariosViewModel herramientasVM = new HerramientasUsuariosViewModel(HttpContext.Session.GetString("Id"), HttpContext.Session.GetString("NombreDeUsuario"), HttpContext.Session.GetString("Rol"));
            return View(herramientasVM);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    private bool isLogged()
    {
        if (HttpContext.Session.GetString("Id") != null) 
            return true;
            
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
