﻿using Microsoft.AspNetCore.Http;
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
        public ActionResult AgregarDetalleAlternativa()
        {


            return View();
        }


        [HttpPost()]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa, IFormFile FacturaFoto, IFormFile Proforma)
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

                    if (FacturaFoto != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await FacturaFoto.CopyToAsync(stream);
                            detalleAlternativa.FacturaFoto = stream.ToArray();
                        }
                    }

                    if (Proforma != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Proforma.CopyToAsync(stream);
                            detalleAlternativa.Proforma = stream.ToArray();
                        }
                    }

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
        public async Task<ActionResult> MontosTotalesPorAlternativa(string AlternativaSeleccionada)
        {
            MontosTotalesViewModel viewModel;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/DetalleAlternativa/MontosTotalesPorAlternativa");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<MontosTotalesViewModel>(apiResponse);

                    if (AlternativaSeleccionada != null)
                    {
                        viewModel.AlternativaSeleccionada = AlternativaSeleccionada;

                        return View(viewModel);
                    }
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
            viewModel = new MontosTotalesViewModel();
            return View(viewModel);
        }
    }


}




