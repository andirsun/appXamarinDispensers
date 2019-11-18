using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class DispensadorModelo : INotifyPropertyChanged
    {

        //Constructor
        #region Constructors
        public DispensadorModelo() { }
        public DispensadorModelo(string nombre, double latitud, double longitud, List<ElementoModelo> elementos)
        {
            this.Nombre = nombre;
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.Distancia = 10;
            this.Elementos = elementos;

        }

        #endregion

        //Atributos
        #region Atributes
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("idDispensador")] 
        public string idDispensador;
        [JsonProperty("nombre")]
        public string nombre;
        [JsonProperty("latitud")]
        public double latitud;
        [JsonProperty("longitud")]
        public double longitud;
        [JsonProperty("id")]
        public long id_Dispensador;
        public double distancia;
        public List<ElementoModelo> elementos;
        #endregion
        
        //Métodos
        #region Properties
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(); }
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
        public long Id_Dispensador
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

    }
}
