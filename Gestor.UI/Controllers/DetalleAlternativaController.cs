using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


       

        [HttpGet]
        public async Task<ActionResult> AgregarDetalleAlternativa(int id)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/ObtenerPorId/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var beneficiario = JsonConvert.DeserializeObject<Beneficiario>(apiResponse);
                    var detalle = new AgregarAlternativaDto() { idBeneficiario = id };
                    return View(detalle);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                    throw new Exception("Error al obtener los detalles del beneficiario");
                }

            }
            catch (Exception ex)
            {
                return View();
            }

        }


        [HttpPost()]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarDetalleAlternativa(AgregarAlternativaDto detalleAlternativa, IFormFile FacturaFoto, IFormFile Proforma)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //mapeo 
                    var detalle = new DetalleAlternativa()
                    {
                        idBeneficiario = detalleAlternativa.idBeneficiario,
                        NombreAlternativa = detalleAlternativa.NombreAlternativa,
                        Fecha = detalleAlternativa.Fecha,
                        Articulo = detalleAlternativa.Articulo,
                        Proveedor = detalleAlternativa.Proveedor,
                        Cantidad = detalleAlternativa.Cantidad,
                        NumeroFactura = detalleAlternativa.NumeroFactura,
                        Monto = detalleAlternativa.Monto

                    };

                    if (FacturaFoto != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await FacturaFoto.CopyToAsync(stream);
                            detalle.FacturaFoto = stream.ToArray();
                        }
                    }

                    if (Proforma != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Proforma.CopyToAsync(stream);
                            detalle.Proforma = stream.ToArray();
                        }
                    }

                    var httpClient = new HttpClient();
                    string json = JsonConvert.SerializeObject(detalle);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var respueta = await httpClient.PostAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/AgregarDetalleAlternativa", byteContent);

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
                //  ----> Primero se encuentra el detalle a editar
                var httpClient = new HttpClient();
                var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api//DetalleAlternativa/{id}");
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                detalleAlternativa = JsonConvert.DeserializeObject<DetalleAlternativa>(apiResponse1);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //  ----> Si existe  el detalle se envia al view

            var detalle = new AgregarAlternativaDto()
            {

                idDetalleAlternativa = detalleAlternativa.idDetalleAlternativa,
                idBeneficiario = detalleAlternativa.idBeneficiario,
                NombreAlternativa = detalleAlternativa.NombreAlternativa,
                Fecha = detalleAlternativa.Fecha,
                Articulo = detalleAlternativa.Articulo,
                Proveedor = detalleAlternativa.Proveedor,
                Cantidad = detalleAlternativa.Cantidad,
                NumeroFactura = detalleAlternativa.NumeroFactura,
                Monto = detalleAlternativa.Monto,
                FotoFacturaActual = detalleAlternativa.FacturaFoto,
                ProformaActual = detalleAlternativa.Proforma
            };

            return View(detalle);
        }

        [HttpPost]
        public async Task<ActionResult> EditarDetalleAlternativa(AgregarAlternativaDto detalleAlternativa,
            IFormFile FacturaFoto, IFormFile Proforma)
        {
            try
            {
                
                

                    DetalleAlternativa detalleOriginal;



                    //  ----> Primero se encuentra el detalle a editar
                    var httpClient = new HttpClient();
                    var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/{detalleAlternativa.idDetalleAlternativa}");
                    string apiResponse1 = await response1.Content.ReadAsStringAsync();
                    detalleOriginal = JsonConvert.DeserializeObject<DetalleAlternativa>(apiResponse1);


                    // ---> Agregamos las nuevas fotos si vienen en el request
                    if (FacturaFoto != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await FacturaFoto.CopyToAsync(stream);
                            detalleOriginal.FacturaFoto = stream.ToArray();
                        }
                    }

                    if (Proforma != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Proforma.CopyToAsync(stream);
                            detalleOriginal.Proforma = stream.ToArray();
                        }
                    }

                    //mapeo 
                    detalleOriginal.NombreAlternativa = detalleAlternativa.NombreAlternativa;
                    detalleOriginal.Fecha = detalleAlternativa.Fecha;
                    detalleOriginal.Articulo = detalleAlternativa.Articulo;
                    detalleOriginal.Cantidad = detalleAlternativa.Cantidad;
                    detalleOriginal.Proveedor = detalleAlternativa.Proveedor;
                    detalleOriginal.Monto = detalleAlternativa.Monto;
                    detalleOriginal.NumeroFactura = detalleAlternativa.NumeroFactura;
                    //detalleOriginal.FacturaFoto = detalleAlternativa.FacturaFoto;
                    //detalleOriginal.Proforma = detalleAlternativa.Proforma;


                    string jsonBeneficiario = JsonConvert.SerializeObject(detalleOriginal);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonBeneficiario);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await httpClient.PutAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/EditarDetalleAlternativa", byteContent);

                    return RedirectToAction(nameof(ListarDetalleAlternativa));


                
                
                
            }
            catch(Exception ex)
            {
                return View();
            }
        }


        public async Task<ActionResult<IEnumerable<DetalleAlternativa>>> ObtenerDetallePorIdBeneficiario(int idBeneficiario)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/ObtenerDetallePorIdBeneficiario/{idBeneficiario}");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List<DetalleAlternativa> detalles = JsonConvert.DeserializeObject<List<DetalleAlternativa>>(apiResponse);

                    if (detalles == null && detalles.Count == 0)
                    {
                        return NotFound("No se encontraron detalles para el beneficiario.");
                    }

                    return View(detalles);
                }
                else
                {
                    // Log the error or handle it appropriately for your application.
                    return StatusCode((int)response.StatusCode, "Error al obtener los detalles del beneficiario");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately for your application.
                return StatusCode(500, "Error interno del servidor al obtener los detalles del beneficiario");
            }
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

            return View(viewModel);
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

            return View(viewModel);
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

            return View(viewModel);
        }

        //montos totales por beneficiario*****************************************************

        [HttpGet]
        public async Task<ActionResult> MontosTotalesPorBeneficiario(int? idBeneficiario = null)
        {
            MontosTotalesViewModel viewModel = new MontosTotalesViewModel();

            try
            {
                // Si no se proporciona un ID de beneficiario seleccionado, utiliza un valor predeterminado o maneja de acuerdo a tus necesidades.
                int beneficiaryId = idBeneficiario ?? 9; // Cambia '1' al valor predeterminado que desees.

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorBeneficiario/{beneficiaryId}");

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);
                    }
                    else
                    {
                        // Maneja el error de una manera adecuada para tu aplicación.
                        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
                return StatusCode(500, "Error interno del servidor");
            }

            return View(viewModel);
        }



        //montos totales por alternativa*****************************************************

        [HttpGet]
        public async Task<IActionResult> MontosTotalesPorAlternativa(string AlternativaSeleccionada)
        {

            MontosTotalesViewModel detalleAlternativa;

            try
            {
                var httpClient = new HttpClient();
                //var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorAlternativa?AlternativaSeleccionada={AlternativaSeleccionada}");

                var response1 = await httpClient.GetAsync($"https://localhost:7229/api/DetalleAlternativa/MontosTotalesPorAlternativa?AlternativaSeleccionada={AlternativaSeleccionada}");
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                detalleAlternativa = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(detalleAlternativa);
        }

    }

}




