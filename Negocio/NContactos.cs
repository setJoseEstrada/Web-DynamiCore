using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;

namespace Negocio
{
    public class NContactos
    {
        List<Contactos> _contactos = 
            new List<Contactos>();
        Contactos _con = new Contactos();
        public List<Contactos> ConsultarTodos()
        {
            try
            {
                using (var cliente = new HttpClient())
                { 
                Task<HttpResponseMessage> respondeHTTP = cliente.GetAsync("https://localhost:44348/api/Contactos");
                respondeHTTP.Wait();

                HttpResponseMessage leerHTTP = respondeHTTP.Result;

                if (leerHTTP.IsSuccessStatusCode)
                {
                    Task<string> asincronoleerhttp = leerHTTP.Content.ReadAsStringAsync();
                    asincronoleerhttp.Wait();

                    string json = asincronoleerhttp.Result;
                    _contactos = JsonConvert.DeserializeObject<List<Contactos>>(json);
                }
                else
                {
                    throw new Exception($"Web Api. Repondio con error.{leerHTTP.StatusCode}");
                }
            }
            }
            catch(Exception ex)
            {
                throw new Exception($"Api no respondio correctamente:{ex.Message}");


    }

            return _contactos;

        }

        public Contactos Consultaruno (int id)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    var responseTask = cliente.GetAsync("https://localhost:44348/api/Contactos" + $"/{id}");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        _con = JsonConvert.DeserializeObject<Contactos>(json);

                    }
                    else
                    {

                        throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI. Respondio con error.{ex.Message}");
            }


            return _con;

        }

        public Contactos Agregar (Contactos objcon)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                   HttpContent content = new StringContent(JsonConvert.SerializeObject(objcon),Encoding.UTF8);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = cliente.PostAsync("https://localhost:44348/api/Contactos", content);
                    postTask.Wait();

                    var result = postTask.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        var read = result.Content.ReadAsStringAsync();
                        read.Wait();
                        string json = read.Result;

                        _con = JsonConvert.DeserializeObject<Contactos>(json);
                    }
                    else
                    {
                        throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"WebAPI. Respondio con error.{ex.Message}");
            }


            return _con;

        }

        public void Modificar(Contactos objcon)
        {
            try
            {
                using (var cliente = new HttpClient())
                {

                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objcon), Encoding.UTF8);

                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                    var postTask = cliente.PutAsync("https://localhost:44348/api/Contactos" + $"/{objcon.id}", httpContent);

                    postTask.Wait();
                    var result = postTask.Result;
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                    }


                }
            }
            catch(Exception ex)
            {
                throw new Exception($"WebAPI. Respondio con error.{ex.Message}");
            }



        }


        public void Eliminar (int id)
        {
            using (var cliente = new HttpClient())
            {
                var respodeTask = cliente.DeleteAsync("https://localhost:44348/api/Contactos" + $"/{id}");
                respodeTask.Wait();
                var result = respodeTask.Result;
                if(!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
        }






    }
}
