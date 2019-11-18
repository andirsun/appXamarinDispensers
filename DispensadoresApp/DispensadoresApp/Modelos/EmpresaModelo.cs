using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DispensadoresApp.ViewModels;

namespace DispensadoresApp.Modelos
{
    public class EmpresaModelo : NotificationObject
    {
        //Atributos
        #region Atributos

        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("id")]
        private int id_Empresa;
        [JsonProperty("nombre")]
        private string nombre;
        [JsonProperty("pathImagenMapa")]
        private string pathImagenMapa;
        [JsonProperty("tiempoMaxReserva")]
        private string tiempoMaxReserva;

        #endregion

        //Métodos

        #region Getters/Setters
        public string PathImagenMapa
        {
            get { return pathImagenMapa; }
            set
            {
                pathImagenMapa = value;
                OnPropertyChanged();
            }
        }
        public string TiempoMaxReserva
        {
            get { return tiempoMaxReserva; }
            set
            {
                tiempoMaxReserva = value;
                OnPropertyChanged();
            }
        }
        public string Nombre
        {
            get { return nombre; }
            set { 
                nombre = value;
                OnPropertyChanged();
                }
        }
        public int Id_Empresa
        {
            get { return id_Empresa; }
            set {
                id_Empresa = value;
                OnPropertyChanged();
                }
        }
     
        #endregion
    }
}
