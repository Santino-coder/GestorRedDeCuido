using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Gestor.SI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
   

    public class RegistroController : Controller
    {


        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender; 

        public RegistroController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Email,Password")] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Input.Email, Email = model.Input.Email };
                var result = await _userManager.CreateAsync(user, model.Input.Password);

                if (result.Succeeded)
                {
                    // Genera un token de confirmación de correo electrónico
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Crea una URL de confirmación de correo electrónico
                    var callbackUrl = Url.Action("ConfirmarEmail", "Cuenta", new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);

                    // Envía un correo electrónico de confirmación con el enlace
                    await _emailSender.SendEmailAsync(model.Input.Email, "Confirmar tu correo electrónico",
                        $"Por favor, confirma tu correo electrónico haciendo clic <a href='{callbackUrl}'>aquí</a>.");

                    // Redirige al usuario a una página de confirmación
                    return View("ConfirmacionEnviada");
                }

                foreach (var error in result.Errors)
                {
                    // Personalizar mensajes de error en español
                    if (error.Code == "PasswordRequiresDigit")
                        ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos un dígito ('0'-'9').");
                    else if (error.Code == "PasswordRequiresLower")
                        ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos una letra minúscula.");
                    else if (error.Code == "PasswordRequiresUpper")
                        ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos una letra mayúscula.");
                    else if (error.Code == "PasswordRequiresNonAlphanumeric")
                        ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos un carácter no alfanumérico.");
                }
            }

            // Si hay errores, muestra el formulario nuevamente
            return View(model);
        }
    }
}
