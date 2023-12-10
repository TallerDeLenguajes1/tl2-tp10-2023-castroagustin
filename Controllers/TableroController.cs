using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class TableroController : Controller
{
    private TableroRepository tableroRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    public IActionResult Index()
    {
        var tableros = tableroRepository.GetAll();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult CreateTablero()
    {
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult CreateTablero(Tablero tablero)
    {
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult UpdateTablero(int id)
    {
        var tablero = tableroRepository.Get(id);
        return View(tablero);
    }
    [HttpPost]
    public IActionResult UpdateTablero(Tablero tablero)
    {
        tableroRepository.Update(tablero.Id, tablero);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult DeleteTablero(int id)
    {
        var tablero = tableroRepository.Get(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult DeleteTablero(Tablero tablero)
    {
        tableroRepository.Remove(tablero.Id);
        return RedirectToAction("Index");
    }
}
