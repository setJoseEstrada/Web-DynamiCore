using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NUsuarios
    {
        List<Usuarios> _usuarios =
           new List<Usuarios>();
        Usuarios _usua = new Usuarios();
        public List<Usuarios> ConsultarTodos()
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    Task<HttpResponseMessage> respondeHTTP = cliente.GetAsync("https://localhost:44348/api/Usuarios");
                    respondeHTTP.Wait();

                    HttpResponseMessage leerHTTP = respondeHTTP.Result;

                    if (leerHTTP.IsSuccessStatusCode)
                    {
                        Task<string> asincronoleerhttp = leerHTTP.Content.ReadAsStringAsync();
                        asincronoleerhttp.Wait();

                        string json = asincronoleerhttp.Result;
                        _usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(json);
                    }
                    else
                    {
                        throw new Exception($"Web Api. Repondio con error.{leerHTTP.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Api no respondio correctamente:{ex.Message}");


            }

            return _usuarios;

        }

       public Usuarios Consultaruno(int id)
        {

            Usuarios usuarios = null;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:44348/api/Usuarios" + $"/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string json = readTask.Result;

                    usuarios = JsonConvert.DeserializeObject<Usuarios>(json);

                }
                else
                {

                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
            return usuarios;

        }


        public Usuarios Agregar(Usuarios objusuarios)
        {
            try
            {
                using (var cliente = new HttpClient())
                {

                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objusuarios), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var posTask = cliente.PostAsync("https://localhost:44348/api/Usuarios",httpContent);
                    posTask.Wait();

                    var result = posTask.Result;

                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();    
                        string json = readTask.Result;
                        objusuarios = JsonConvert.DeserializeObject<Usuarios>(json);
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
            return objusuarios;
        }

        public void Eliminar(int id)
        {
            using (var cliente = new HttpClient())
            {
                var respondeTask = cliente.DeleteAsync("https://localhost:44348/api/Usuarios" + $"/{id}");
                respondeTask.Wait();
                var result = respondeTask.Result;
                if(!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
        }


        public void Modificar(Usuarios objusuarios)
        {
            using (var cliente = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(objusuarios),Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var puttTask = cliente.PutAsync("https://localhost:44348/api/Usuarios" + $"/{objusuarios.id}", httpContent);
                puttTask.Wait();

                var result = puttTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"WebAPI. Respondio con error.{result.StatusCode}");
                }
            }
        }
    }
}



