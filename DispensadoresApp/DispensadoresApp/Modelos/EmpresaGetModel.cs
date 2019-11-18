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

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}
