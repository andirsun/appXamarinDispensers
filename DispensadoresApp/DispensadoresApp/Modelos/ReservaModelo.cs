using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DispensadoresApp.ViewModels;

namespace DispensadoresApp.Modelos
{
    public class ReservaModelo : NotificationObject
    {
        public ReservaModelo()//constructor 
        {

        }
        //Atributos
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        
        [JsonProperty("idUsuario")]
        public int idUsuario;
        [JsonProperty("idUbicacion")]
        public int idUbicacion;
        [JsonProperty("idElemento")]
        public int idElemento;
        #endregion

        #region Getters/Setters

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; OnPropertyChanged(); }
        }
        public int IdUbicacion
        {
            get { return idUbicacion;  }
            set { idUbicacion = value; OnPropertyChanged(); }
        }
        public int IdElemento
        {
            get { return idElemento; }
            set { idUbicacion = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
