using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NDirectorio
    {

        public List<Directorio> ConsultarTodos(string token)
        {
            List<Directorio> _list = null;
            try
            {
                using (var cliente = new HttpClient())
                {

                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> httpResponse = cliente.GetAsync("https://localhost:44348/api/Directorios");

                    httpResponse.Wait();

                    HttpResponseMessage httpResponseMessage = httpResponse.Result;

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        Task<string> asincronoleerhttp = httpResponseMessage.Content.ReadAsStringAsync();
                        asincronoleerhttp.Wait();

                        string json = asincronoleerhttp.Result;

                        _list = JsonConvert.DeserializeObject<List<Directorio>>(json);
                    }
                    else
                    {

                        throw new Exception($"Web Api. Repondio con error.{httpResponseMessage.StatusCode}");
                    }


                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Api no respondio correctamente:{ex.Message}");
            }


            return _list;
        }


        public Directorio Agregar(Directorio objDirectorio)
        {
            Directorio _objDirectorio = new Directorio();
            try
            {
                using (var cliente = new HttpClient())
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objDirectorio), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var postTask = cliente.PostAsync("https://localhost:44348/api/Directorios", httpContent);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (postTask.IsCompleted)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string json = readTask.Result;

                        _objDirectorio = JsonConvert.DeserializeObject<Directorio>(json);
                    }
                    else
                    {
                        throw new Exception($"{result.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return _objDirectorio;


        }


        public void Modificar(Directorio objdirectorio)
        {
            using (var cliente = new HttpClient())
            {

                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objdirectorio), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var postTask = cliente.PutAsync("https://localhost:44348/api/Directorios" + $"/{objdirectorio.id}", httpContent);
                postTask.Wait();

                var result = postTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");

                }

            }
        }

        public Directorio Consultaruno(int id)
        {
            Directorio objdirectorio = new Directorio();

            using (var cliente = new HttpClient())
            {

                var responTask = cliente.GetAsync("https://localhost:44348/api/Directorios" + $"/{id}");
                responTask.Wait();
                var result = responTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;
                    objdirectorio = JsonConvert.DeserializeObject<Directorio>(json);

                }
                else
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }

            }
            return objdirectorio;
        }

        public void  Eliminar (int id)
        {
            using (var cliente = new HttpClient())
            {
                var responseTask = cliente.DeleteAsync("https://localhost:44348/api/Directorios" + $"/{id}");


                responseTask.Wait();

                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }

            }
        }


    }
}
