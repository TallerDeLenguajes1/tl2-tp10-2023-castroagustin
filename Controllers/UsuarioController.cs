using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly ITareaRepository _tareaRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        try
        {
            return View(new CrearUsuarioViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CreateUser(CrearUsuarioViewModel usuario)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("CreateUser");
            usuario.Rol = Roles.operador;
            _usuarioRepository.Create(new Usuario(usuario));
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult UpdateUser(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var user = _usuarioRepository.Get(id);
            return View(new ModificarUsuarioViewModel(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult UpdateUser(ModificarUsuarioViewModel usuario)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");
            _usuarioRepository.Update(usuario.Id, new Usuario(usuario));
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteUser(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("CreateUser");

            _usuarioRepository.Remove(id);
            var tableros = _tableroRepository.GetAllByUser(id);
            foreach (var t in tableros)
            {
                _tableroRepository.Remove(t.Id);
                var tareas = _tareaRepository.GetAllByTablero(t.Id);
                foreach (var tarea in tareas)
                {
                    _tareaRepository.Remove(tarea.Id);
                }
            }

            var tareasAsignadas = _tareaRepository.GetAllByUser(id);
            foreach (var tarea in tareasAsignadas)
            {
                var tareaMod = tarea;
                tareaMod.IdUsuarioAsignado = null;
                _tareaRepository.Update(tarea.Id, tareaMod);
            }
            if (id == (int)HttpContext.Session.GetInt32("id")) return RedirectToRoute(new { controller = "Login", action = "Index" });
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
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