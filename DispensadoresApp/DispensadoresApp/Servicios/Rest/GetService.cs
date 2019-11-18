using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using DispensadoresApp.Servicios.Rest;
using Xamarin.Forms;

namespace DispensadoresApp.Servicios
{
    public class GetService<T> : IRestService<T>
    {
        //Atributos
        private readonly string Url;

        //Constructor
        public GetService(string Url)
        {
            this.Url = Url;
        }

        //Métodos
        public async Task<Tuple<bool, string>> SendRequest(T model)//lista de string para armar la url
        {
            bool isAuthorized = false;
            string response = "";
            string urlParameters = this.Url + model; //Interpretas parámetros

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
                    if (Application.Current.Properties.ContainsKey("token"))//despues de la 2 vez de logeado la primera vez ya pone el token en los headers

                    {
                        string token = Application.Current.Properties["token"] as string;
                        string id = Application.Current.Properties["UserID"] as string;
                        string fulltoken = "Bearer " + token;
                        string fullid = "ID " + id;
                        requestMessage.Headers.Add("Authorization",fulltoken);
                        requestMessage.Headers.Add("UserID",fullid);
                    }
                    
                    //client.Timeout = TimeSpan.FromSeconds(10);
                    HttpResponseMessage HttpResponse = await client.SendAsync(requestMessage);
                    isAuthorized = HttpResponse.IsSuccessStatusCode;//1 si la el status fue exitoso de la peticion 
                    response = await HttpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                response = "Error de conexión, por favor reintente";
            }

            Tuple<bool, string> answer = Tuple.Create(isAuthorized, response);
            return answer;
        }
    }

}
