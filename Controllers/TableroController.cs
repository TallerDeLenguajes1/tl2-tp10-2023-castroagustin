using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class TableroController : Controller
{
    private TableroRepository tableroRepository;
    private TareaRepository tareaRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
        tareaRepository = new TareaRepository();
    }

    public IActionResult Index()
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (esAdmin())
        {
            var tableros = tableroRepository.GetAll();
            return View(new ListarTablerosViewModel(tableros));
        }
        else
        {
            var tableros = tableroRepository.GetAllByUser((int)HttpContext.Session.GetInt32("id"));
            return View(new ListarTablerosViewModel(tableros));
        }
    }

    [HttpGet]
    public IActionResult CreateTablero()
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        return View(new CrearTableroViewModel { IdUsuarioPropietario = (int)HttpContext.Session.GetInt32("id") });
    }

    [HttpPost]
    public IActionResult CreateTablero(CrearTableroViewModel tablero)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        tableroRepository.Create(new Tablero(tablero));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateTablero(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        var tablero = tableroRepository.Get(id);
        return View(new ModificarTableroViewModel(tablero));
    }
    [HttpPost]
    public IActionResult UpdateTablero(ModificarTableroViewModel tablero)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        tableroRepository.Update(tablero.Id, new Tablero(tablero));
        return RedirectToAction("Index");
    }


    public IActionResult DeleteTablero(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!esAdmin())
        {
            var tablero = tableroRepository.Get(id);
            if ((int)HttpContext.Session.GetInt32("id") != tablero.IdUsuarioPropietario) return RedirectToAction("Index");
        }
        var tareas = tareaRepository.GetAllByTablero(id);
        foreach (var tarea in tareas)
        {
            tareaRepository.Remove(tarea.Id);
        }
        tableroRepository.Remove(id);
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
