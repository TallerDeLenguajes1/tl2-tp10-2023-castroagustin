using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (esAdmin())
        {
            var usuarios = _usuarioRepository.GetAll();
            return View(new ListarUsuariosViewModel(usuarios));
        }
        else
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(_usuarioRepository.Get((int)HttpContext.Session.GetInt32("id")));
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
        if (!ModelState.IsValid) return RedirectToAction("CreateUser");
        _usuarioRepository.Create(new Usuario(usuario));
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateUser(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        var user = _usuarioRepository.Get(id);
        return View(new ModificarUsuarioViewModel(user));
    }

    [HttpPost]
    public IActionResult UpdateUser(ModificarUsuarioViewModel usuario)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!ModelState.IsValid) return RedirectToAction("Index");
        _usuarioRepository.Update(usuario.Id, new Usuario(usuario));
        return RedirectToAction("Index");
    }

    public IActionResult DeleteUser(int id)
    {
        if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        if (!ModelState.IsValid) return RedirectToAction("CreateUser");

        _usuarioRepository.Remove(id);
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