using Gestor.Models;
using Gestor.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gestor.UI.Controllers
{
   // [Authorize]
    public class BeneficiarioController : Controller
    {
        public BeneficiarioController()
        {

        }

        public async Task<IActionResult> ListarBeneficiario()
        {
            List<Beneficiario> listaBeneficiarios;

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/ListarBeneficiario");

               
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   
                listaBeneficiarios = JsonConvert.DeserializeObject<List<Beneficiario>>(apiResponse);
               
                   

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(listaBeneficiarios);
        }

        [HttpGet]
        public ActionResult AgregarBeneficiario()
        {


          return View();
        }

        //[HttpPost]

        //public async Task<IActionResult> Test(Beneficiario beneficiario)
        //{
        //    var datos = beneficiario;
        //    return Ok("Sirve");
        //}


         [HttpPost()]
   
        public async Task<IActionResult> AgregarBeneficiario(Beneficiario beneficiario)
        {
            try
            {
                ModelState.Remove("DetalleAlternativa");

                if (ModelState.IsValid)
                {
                    var httpClient = new HttpClient();
                    string json = JsonConvert.SerializeObject(beneficiario);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/AgregarBeneficiario", byteContent);


                    return RedirectToAction(nameof(ListarBeneficiario));
                }
                else
                {
                    return View();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                return View(ex);
            }
        }


        public async Task<IActionResult> Detalles(int id)
        {
            Beneficiario beneficiario;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/ObtenerPorId/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    beneficiario = JsonConvert.DeserializeObject<Beneficiario>(apiResponse);
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

            return View(beneficiario);
        }

        public async Task<IActionResult> EditarBeneficiario(int id)
        {
           
            Beneficiario beneficiario;

            try
            {
                var httpClient = new HttpClient();
                var response1 = await httpClient.GetAsync($"https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/ObtenerPorId/{id}");
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
        public async Task<ActionResult> EditarBeneficiario(Beneficiario beneficiario)
        {
            try
            {
                if (ModelState.IsValid) {

                    var httpClient = new HttpClient();
                    string jsonBeneficiario = JsonConvert.SerializeObject(beneficiario);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonBeneficiario);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await httpClient.PutAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/EditarBeneficiario/{beneficiario.Id}", byteContent);
                    return RedirectToAction(nameof(ListarBeneficiario));


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

        public async Task<IActionResult> CantidadTotalBeneficiarios()
        {
            List<Beneficiario> listaBeneficiarios;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://reddecuido-hojancha-si.azurewebsites.net/api/Beneficiario/CantidadTotalBeneficiarios");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaBeneficiarios = JsonConvert.DeserializeObject<List<Beneficiario>>(apiResponse);
                }
                else
                {
                    // Maneja el error de una manera adecuada para tu aplicación.
                    throw new Exception("Error al obtener la lista de beneficiarios");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            int cantidadTotal = listaBeneficiarios.Count;
            int cantidadActivos = listaBeneficiarios.Count(b => b.Estado == "Activo");
            int cantidadInactivos = listaBeneficiarios.Count(b => b.Estado == "Inactivo");
            int cantidadFallecidos = listaBeneficiarios.Count(b => b.Estado == "Fallecido");

            ViewBag.CantidadActivos = cantidadActivos;
            ViewBag.CantidadInactivos = cantidadInactivos;
            ViewBag.CantidadFallecidos = cantidadFallecidos;

            return View(cantidadTotal);
        }

    }
}
