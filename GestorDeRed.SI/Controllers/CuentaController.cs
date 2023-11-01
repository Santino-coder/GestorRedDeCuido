using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeRed.SI.Controllers
{
    public class CuentaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CuentaController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Manejar el caso de usuario no encontrado
                return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado." });
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result;
        }
    }
}
