using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DispensadoresApp.Modelos
{
    public class EmpresaGetModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tiempoMaxReserva")]
        public string tiempoMaxReserva { get; set; }
    
    }
}
