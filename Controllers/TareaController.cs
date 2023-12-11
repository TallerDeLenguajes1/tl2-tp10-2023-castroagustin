using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class TareaController : Controller
{
    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private TableroRepository tableroRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
    }

    public IActionResult Index(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        /* if (esAdmin())
        {
            var tareas = _tareaRepository.GetAll();
            return View(new ListarTareasViewModel(tareas));
        }
        else
        {
            var tareas = _tareaRepository.GetAllByUser((int)HttpContext.Session.GetInt32("id"));
            return View(new ListarTareasViewModel(tareas));
        } */

        if (!esAdmin())
        {
            int idUser = (int)HttpContext.Session.GetInt32("id");
            var tareas = _tareaRepository.GetAllByTablero(id);
            var tableros = _tableroRepository.GetAll();
            if (tableros.Any(t => t.Id == id))
            {
                return View(new ListarTareasViewModel(_tareaRepository.GetAllByTablero(id)));
            }
        }
        else
        {
            return View(new ListarTareasViewModel(_tareaRepository.GetAllByTablero(id)));
        }
        return RedirectToRoute(new { controller = "Tablero", action = "Index" });
    }

    [HttpGet]
    public IActionResult CreateTarea(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        return View(new CrearTareaViewModel { IdTablero = id });
    }

    [HttpPost]
    public IActionResult CreateTarea(CrearTareaViewModel tarea)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!ModelState.IsValid) return RedirectToAction("Index", new { id = tarea.IdTablero });
        _tareaRepository.Create(tarea.IdTablero, new Tarea(tarea));
        return RedirectToAction("Index", new { id = tarea.IdTablero });
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!esAdmin())
        {
            var idUsuario = (int)HttpContext.Session.GetInt32("id");
            var tarea = _tareaRepository.Get(id);
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
            var tarea = _tareaRepository.Get(id);
            return View(new ModificarTareaViewModel(tarea));
        }
    }

    [HttpPost]
    public IActionResult UpdateTarea(ModificarTareaViewModel tarea)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!ModelState.IsValid) return RedirectToAction("Index", new { id = tarea.IdTablero });
        _tareaRepository.Update(tarea.Id, new Tarea(tarea));
        return RedirectToAction("Index");
    }


    public IActionResult DeleteTarea(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!ModelState.IsValid) return RedirectToAction("Index", id);

        var idTablero = _tareaRepository.Get(id).IdTablero;
        _tareaRepository.Remove(id);
        return RedirectToAction("Index", new { id = idTablero });
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
