using Entidades;
using Microsoft.Win32;
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
    public class NLogin
    {

        public Respuesta Login(Login objLogin)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (var cliente = new HttpClient())
                {
                    HttpContent contenido = new StringContent(JsonConvert.SerializeObject(objLogin),Encoding.UTF8);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    Task<HttpResponseMessage> httpResponseMessage = cliente.PostAsync("https://localhost:44348/api/User/login", contenido);

                    httpResponseMessage.Wait();

                    var httpmessage = httpResponseMessage.Result;
                    if (httpmessage.IsSuccessStatusCode)
                    {
                        Task<string> tarea = httpmessage.Content.ReadAsStringAsync();
                         tarea.Wait();  
                        string json = tarea.Result; 
                        respuesta = JsonConvert.DeserializeObject<Respuesta>(json);
                    }
                    else
                        throw new Exception($"Api no responde correctamente: {httpmessage.StatusCode}");
                }
            }
            catch(Exception ex)
            {
                respuesta.Mensaje = "Usuario o Contraseña incorrecta";
            }


            return respuesta;
        }

    }
}
