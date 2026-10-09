using Laboratorio4.Hubs;
using Laboratorio4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Laboratorio4.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository  _usuarioRepository;
    private readonly IHubContext<LoginConVerificacionHub> _hub;
    
    
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository,IHubContext<LoginConVerificacionHub> hub)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
        _hub = hub;
    }
    

    [HttpPost]
    public IActionResult ValidaLogin(String email, String password,string connectionId)
    {
        _logger.LogInformation("Busco al usuario");
        Usuario? usuario = _usuarioRepository.Buscar(email);

        bool valido = usuario != null && usuario.Password == password;
        
        if (valido)
        {
            string? enlace = Url.Action(nameof(Confirmar), "Login", new { connectionId }, Request.Scheme);
            _logger.LogInformation("Enlace de confirmación: {Enlace}", enlace);
        }
        
        return Json(new { valido });
    }
    
    [HttpGet]
    public async Task<IActionResult> Confirmar(string connectionId)
    {
        //espera a que el cliente ingrese al link luego ejecuta la VerificacionOk
        await _hub.Clients.Client(connectionId)
            .SendAsync("VerificacionOk");

        return Content("Confirmación realizada. Podés cerrar esta pestaña.");
    }
}