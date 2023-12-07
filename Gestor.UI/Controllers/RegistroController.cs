namespace Gestor.UI.Controllers
{
    using Gestor.UI.Areas.Identity.Pages.Account;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class RegistroController : Controller
    {
        public IActionResult Registrar([FromBody] RegistroModel model)
        {
            if (ModelState.IsValid)
            {
            
                var user = new { UserName = model.Email, Email = model.Email }; 

                // Genera un token de confirmación de correo electrónico (simulado)
                var token = "token_de_confirmacion_generado";

                // Crea una URL de confirmación de correo electrónico
                var callbackUrl = $"https://reddecuido-hojancha-si.azurewebsites.net/api/Registro={user.UserName}&code={token}";

               
                return Ok("Usuario registrado con éxito");
            }

            // Si hay errores, devuelve los errores al cliente
            return BadRequest(ModelState);
        }
    }

    public class RegistroModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}


