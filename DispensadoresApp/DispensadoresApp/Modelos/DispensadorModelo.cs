using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DispensadoresApp.ViewModels;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class DispensadorModelo : NotificationObject
    {
        //Atributos
        #region Atributes
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("idDispensador")] 
        private string idDispensador;
        [JsonProperty("nombre")]
        private string nombre;
        [JsonProperty("latitud")]
        private double latitud;
        [JsonProperty("longitud")]
        private double longitud;
        [JsonProperty("id")]
        private int id_Dispensador;
        [JsonProperty("numEquipos")]
        private int numEquipos;
        private double distancia;
        private List<ElementoModelo> elementos;
        #endregion
        
        //Métodos
        #region Properties
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(); }
        }
        public int NumEquipos
        {
            get { return numEquipos; }
            set { numEquipos = value;OnPropertyChanged(); }
        }
        public double Latitud
        {
            get { return latitud; }
            set { latitud = value; OnPropertyChanged(); }
        }
        public double Longitud
        {
            get { return longitud; }
            set { longitud = value; OnPropertyChanged(); }
        }
        public int Id_Dispensador
        {
            get { return id_Dispensador; }
            set { id_Dispensador = value; OnPropertyChanged(); }
        }
        public double Distancia
        {
            get { return distancia; }
            set { distancia = value; OnPropertyChanged(); }
        }

        public List<ElementoModelo> Elementos
        {
            get { return elementos; }
            set { elementos = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
