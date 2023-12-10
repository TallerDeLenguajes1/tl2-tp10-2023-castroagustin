using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class TareaController : Controller
{
    private TareaRepository tareaRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    public IActionResult Index()
    {
        var tareas = tareaRepository.GetAll();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult CreateTarea()
    {
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult CreateTarea(Tarea tarea)
    {
        tareaRepository.Create(tarea.IdTablero, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id)
    {
        var tarea = tareaRepository.Get(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult UpdateTarea(Tarea tarea)
    {
        tareaRepository.Update(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteTarea(int id)
    {
        var tarea = tareaRepository.Get(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult DeleteTarea(Tarea tarea)
    {
        tareaRepository.Remove(tarea.Id);
        return RedirectToAction("Index");
    }
}
