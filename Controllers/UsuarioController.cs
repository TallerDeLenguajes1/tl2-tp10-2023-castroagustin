using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class UsuarioController : Controller
{
    private UsuarioRepository usuarioRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        var usuarios = usuarioRepository.GetAll();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CreateUser(Usuario usuario)
    {
        usuarioRepository.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateUser(int idUser)
    {
        var user = usuarioRepository.Get(idUser);
        return View(user);
    }

    [HttpPost]
    public IActionResult UpdateUser(Usuario usuario)
    {
        usuarioRepository.Update(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteUser(int idUser)
    {
        var user = usuarioRepository.Get(idUser);
        return View(user);
    }

    [HttpPost]
    public IActionResult DeleteUser(Usuario usuario)
    {
        usuarioRepository.Remove(usuario.Id);
        return RedirectToAction("Index");
    }
}
