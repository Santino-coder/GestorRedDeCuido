using Gestor.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gestor.UI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public async Task<IActionResult> Index()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiUrl = "https://reddecuido-hojancha.azurewebsites.net/api/Home/Index";
                    var response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        // Aquí puedes procesar la respuesta, por ejemplo, deserializarla si es JSON.
                        // Ejemplo de deserialización de JSON:
                        // var listaBeneficiarios = JsonConvert.DeserializeObject<List<Beneficiario>>(apiResponse);

                        // Luego, puedes usar los datos como lo necesites.

                        return View(); // Retorna la vista adecuada o un modelo con los datos.
                    }
                    else
                    {
                        // Maneja el error de una manera adecuada para tu aplicación.
                        throw new Exception($"Error al hacer la solicitud. Código de estado: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja los errores de una manera adecuada para tu aplicación.
                throw ex;
            }
        }


        public async Task<IActionResult> Privacy()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiUrl = "https://reddecuido-hojancha.azurewebsites.net/api/Home/Privacy";
                    var response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string privacyData = await response.Content.ReadAsStringAsync();
                        return Ok(privacyData);
                    }
                    else
                    {
                        // Maneja el error de una manera adecuada para tu aplicación.
                        throw new Exception("Error al obtener la política de privacidad");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> Error()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string apiUrl = "https://reddecuido-hojancha.azurewebsites.net/api/Home/Error";
                    var response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string errorData = await response.Content.ReadAsStringAsync();
                        return Ok(errorData);
                    }
                    else
                    {
                        // Maneja el error de una manera adecuada para tu aplicación.
                        throw new Exception("Error al obtener información de error");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}