using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace DispensadoresApp.Models
{
    public class UsuarioModelo
    {
        public UsuarioModelo()
        {

        }
        //Atributos
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }

        [JsonProperty("id")]
        public long Id_Usuario { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("nombres")]
        public string Nombre { get; set; }
        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }
        [JsonProperty("apiToken")]
        public string Api_token { get; set; }

        #endregion

        //Métodos

        #region Getters/Setters


        #endregion


    }
}
