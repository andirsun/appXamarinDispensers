using DispensadoresApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DispensadoresApp.Servicios
{
    public class LoginApiRest
    {
        private readonly HttpClient client;

        public LoginApiRest()
        {
            if (client == null) { client = new HttpClient(); }
        }

        public async Task<UsuarioModelo> LoginUsuario(UsuarioModelo usuario)
        {
            UsuarioModelo usuario_Logueado = new UsuarioModelo();
            try
            {
                var jsonCliente = await Task.Run(() => JsonConvert.SerializeObject(usuario));
                var httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                string url = GlobalSettings.BASE_URL + GlobalSettings.LOGIN_URL;
                var httpResponse = await client.PostAsync(url, httpContent);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    usuario_Logueado = JsonConvert.DeserializeObject<UsuarioModelo>(responseContent);

                }

            }
            catch (Exception e)
            {

            }

            return usuario_Logueado;

        }
    }
}
