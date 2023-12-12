using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_castroagustin.Models;
using tl2_tp10_2023_castroagustin.Repositorios;
using tl2_tp10_2023_castroagustin.ViewModels;

namespace tl2_tp10_2023_castroagustin.Controllers;

public class LoginController : Controller
{
    private IUsuarioRepository _usuarioRepository;
    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(LoginViewModel usuario)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            //existe el usuario?
            var usuarioLogeado = _usuarioRepository.GetAll().FirstOrDefault(u => u.NombreDeUsuario == usuario.Nombre && u.Contrasenia == usuario.Contrasenia);

            // si el usuario no existe devuelvo al index
            if (usuarioLogeado == null)
            {
                _logger.LogWarning("Intento de acceso invalido - Usuario: {0} Clave ingresada: {1}", usuario.Nombre, usuario.Contrasenia);
                return RedirectToAction("Index");
            }

            //Registro el usuario
            logearUsuario(usuarioLogeado);

            _logger.LogInformation("El usuario {0} ingreso correctamente", usuarioLogeado.NombreDeUsuario);

            //Devuelvo el usuario al Home
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    private void logearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetInt32("id", usuario.Id);
        HttpContext.Session.SetString("usuario", usuario.NombreDeUsuario);
        HttpContext.Session.SetString("rol", usuario.Rol.ToString());
    }
}