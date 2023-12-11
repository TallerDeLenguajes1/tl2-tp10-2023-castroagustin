using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class TareaController : Controller
{
    private TareaRepository tareaRepository;
    private TableroRepository tableroRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
        tableroRepository = new TableroRepository();
    }

    public IActionResult Index()
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (esAdmin())
        {
            var tareas = tareaRepository.GetAll();
            return View(new ListarTareasViewModel(tareas));
        }
        else
        {
            var tareas = tareaRepository.GetAllByUser((int)HttpContext.Session.GetInt32("id"));
            return View(new ListarTareasViewModel(tareas));
        }
    }

    [HttpGet]
    public IActionResult CreateTarea()
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult CreateTarea(CrearTareaViewModel tarea)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        tareaRepository.Create(tarea.IdTablero, new Tarea(tarea));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!esAdmin())
        {
            var idUsuario = (int)HttpContext.Session.GetInt32("id");
            var tarea = tareaRepository.Get(id);
            var tableros = tableroRepository.GetAll();
            if (tableros.FirstOrDefault(t => t.Id == tarea.IdTablero && tarea.IdUsuarioAsignado == idUsuario) != null)
            {
                return View(new ModificarTareaViewModel(tarea));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        else
        {
            var tarea = tareaRepository.Get(id);
            return View(new ModificarTareaViewModel(tarea));
        }
    }

    [HttpPost]
    public IActionResult UpdateTarea(ModificarTareaViewModel tarea)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        tareaRepository.Update(tarea.Id, new Tarea(tarea));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteTarea(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        var tarea = tareaRepository.Get(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult DeleteTarea(Tarea tarea)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        tareaRepository.Remove(tarea.Id);
        return RedirectToAction("Index");
    }

    private bool logueado()
    {
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin()
    {
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("rol") == Roles.administrador.ToString();
    }
}
