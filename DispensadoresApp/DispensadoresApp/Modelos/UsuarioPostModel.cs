using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace DispensadoresApp.Modelos
{
    public class UsuarioPostModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("idEmpresa")]
        public int IdEmpresa { get; set; }

    }
}
