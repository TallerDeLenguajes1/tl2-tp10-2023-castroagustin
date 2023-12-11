using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

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
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (esAdmin())
        {
            var usuarios = usuarioRepository.GetAll();
            return View(new ListarUsuariosViewModel(usuarios));
        }
        else
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuarioRepository.Get((int)HttpContext.Session.GetInt32("id")));
            return View(new ListarUsuariosViewModel(usuarios));
        }
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult CreateUser(CrearUsuarioViewModel usuario)
    {
        usuarioRepository.Create(new Usuario(usuario));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateUser(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        var user = usuarioRepository.Get(id);
        return View(new ModificarUsuarioViewModel(user));
    }

    [HttpPost]
    public IActionResult UpdateUser(ModificarUsuarioViewModel usuario)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        usuarioRepository.Update(usuario.Id, new Usuario(usuario));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteUser(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        var user = usuarioRepository.Get(id);
        return View(user);
    }

    [HttpPost]
    public IActionResult DeleteUser(Usuario usuario)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        usuarioRepository.Remove(usuario.Id);
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