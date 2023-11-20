using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestor.BS;
using Gestor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gestor.UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Gestor.UI.Controllers
{
    public class DetalleAlternativaController : Controller
    {
        public DetalleAlternativaController()
        {

        }


        public ActionResult AgregarDetalleAlternativa()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var httpClient = new HttpClient();
                    string json = JsonConvert.SerializeObject(detalleAlternativa);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/AgregarDetalleAlternativa", byteContent);


                    return RedirectToAction(nameof(ListarDetalleAlternativa));
                }
                else
                {
                    return View();
                }


            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public async Task<IActionResult> ListarDetalleAlternativa()
        {
            List<DetalleAlternativa> listadetallealternativa;

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/ListarDetalleAlternativa");


                string apiResponse = await response.Content.ReadAsStringAsync();

                listadetallealternativa = JsonConvert.DeserializeObject<List<DetalleAlternativa>>(apiResponse);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(listadetallealternativa);
        }



        public async Task<IActionResult> EditarDetalleAlternativa(int id)
        {

            DetalleAlternativa detalleAlternativa;

            try
            {
                var httpClient = new HttpClient();
                var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/EditarDetalleAlternativa/{id}");
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                detalleAlternativa = JsonConvert.DeserializeObject<DetalleAlternativa>(apiResponse1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(detalleAlternativa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var httpClient = new HttpClient();
                    string jsonBeneficiario = JsonConvert.SerializeObject(detalleAlternativa);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonBeneficiario);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await httpClient.PutAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/EditarDetalleAlternativa", byteContent);
                    return RedirectToAction(nameof(ListarDetalleAlternativa));


                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }



        public async Task<IActionResult> ObtenerDetallePorIdBeneficiario(int id)
        {
            DetalleAlternativa detalleAlternativa;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/ObtenerDetallePorIdBeneficiario/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    detalleAlternativa = JsonConvert.DeserializeObject<DetalleAlternativa>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                    throw new Exception("Error al obtener los detalles del beneficiario");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(detalleAlternativa);
        }



        public async Task<IActionResult> SeleccionarBeneficiario(int id)
        {
            return Ok(new { IdBeneficiarioSeleccionado = id });
            //https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/SeleccionarBeneficiario/
        }




        public async Task<ActionResult> ListarBeneficiario()
        {
            List<Beneficiario> listaBeneficiarios = new List<Beneficiario>();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/ListarBeneficiario");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaBeneficiarios = JsonConvert.DeserializeObject<List<Beneficiario>>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
            }

            return Ok(listaBeneficiarios);
        }





        public async Task<ActionResult> MontosTotales()
        {
            MontosTotalesViewModel viewModel = new MontosTotalesViewModel();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotales");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
            }

            return Ok(viewModel);
        }


        public async Task<ActionResult> MontosTotalesPorMes()
        {
            MontosTotalesViewModel viewModel = new MontosTotalesViewModel();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorMes");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
            }

            return Ok(viewModel);
        }

        //montos totales por año****************************************************************
        public async Task<ActionResult> MontosTotalesPorAnio()
        {
            MontosTotalesViewModel viewModel = new MontosTotalesViewModel();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorAnio");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
            }

            return Ok(viewModel);
        }

        //Montos totales por beneficiario*****************************************************
        public async Task<IActionResult> MontosTotalesPorBeneficiario(int id)
        {

            Beneficiario beneficiario;

            try
            {
                var httpClient = new HttpClient();
                var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorBeneficiario?idBeneficiario={id}");
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                beneficiario = JsonConvert.DeserializeObject<Beneficiario>(apiResponse1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(beneficiario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MontosTotalesPorBeneficiario()
        {
            MontosTotalesViewModel viewModel = new MontosTotalesViewModel();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorBeneficiario");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
            }

            return Ok(viewModel);
        }



        //public async Task<ActionResult<MontosTotalesViewModel>> MontosTotalesPorAlternativa()
        // no se si seren asi


        //montos totales por alternativa*****************************************************

        public async Task<IActionResult> MontosTotalesPorAlternativa(int id)
        {

            DetalleAlternativa detalleAlternativa;

            try
            {
                var httpClient = new HttpClient();
                var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorAlternativa?AlternativaSeleccionada={id}");
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                detalleAlternativa = JsonConvert.DeserializeObject<DetalleAlternativa>(apiResponse1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(detalleAlternativa);
        }
        public async Task<ActionResult> MontosTotalesPorAlternativa()
        {
            MontosTotalesViewModel viewModel = new MontosTotalesViewModel();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorAlternativa");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
            }

            return Ok(viewModel);
        }


      

    }

    
}




