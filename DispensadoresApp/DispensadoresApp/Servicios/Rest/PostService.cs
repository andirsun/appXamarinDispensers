using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DispensadoresApp.Servicios.Rest
{
    public class PostService<T> : IRestService<T>
    {
        //Atributos
        private readonly string Url;

        //Constructor
        public PostService(string Url)
        {
            this.Url = Url;
        }

        //Métodos
        public async Task<Tuple<bool, string>> SendRequest(T model)
        {
            bool isAuthorized = false;
            string response = "";
            string clienteJson = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, Url);
                    if(Application.Current.Properties.ContainsKey("token"))
                    {
                        string token = Application.Current.Properties["token"] as string;
                        string id = Application.Current.Properties["UserID"] as string;
                        requestMessage.Headers.Add("Authorization", token);
                        requestMessage.Headers.Add("UserID", id);
                    }

            
                    requestMessage.Content = content;
                    //client.Timeout = TimeSpan.FromSeconds(10); by define
                    HttpResponseMessage HttpResponse = await client.SendAsync(requestMessage);
                    isAuthorized = HttpResponse.IsSuccessStatusCode;
                    response = await HttpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                response = "Error de conexión, por favor reintente";
            }

            Tuple<bool, string> answer = Tuple.Create(isAuthorized, response);
            return answer;
        }
    }

}
