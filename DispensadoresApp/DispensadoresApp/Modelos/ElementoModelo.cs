using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using SQLite;
using DispensadoresApp.ViewModels;

namespace DispensadoresApp.Modelos
{
    public class ElementoModelo : NotificationObject
    {

        
       
        //Atributo
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("id")]
        private int id;
        [JsonProperty("nombreElemento")]
        private string nombreElemento;
        [JsonProperty("descripcion")]
        private string descripcion;
        [JsonProperty("idUbicacion")]
        private int idUbicacion;
        [JsonProperty("idDispensador")]
        private int idDispensador;
        [JsonProperty("nombreDispensador")]
        private string nombreDispensador;
        [JsonProperty("numCasillero")]
        private int numCasillero;
        [JsonProperty("latitud")]
        private double latitud;
        [JsonProperty("longitud")]
        private double longitud;

        [JsonProperty("distancia")]
        private double distancia;
        



        /*[JsonProperty("tipoElemento")]
        private string tipoElemento;
        [JsonProperty("estadoUbicacion")]
        private int estadoUbicacion;*/

        private string nombre;
        private string path_Imagen;
        private int estado;
        #endregion

        //Métodos

        #region Getters/Setters
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        public string NombreElemento
        {
            get { return nombreElemento; }
            set { nombreElemento = value; OnPropertyChanged(); }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; OnPropertyChanged(); }
        }
        public int IdUbicacion
        {
            get { return idUbicacion; }
            set { idUbicacion = value; OnPropertyChanged(); }
        }
        public int IdDispensador
        {
            get { return idDispensador; }
            set { idDispensador = value; OnPropertyChanged(); }
        }
        public string NombreDispensador
        {
            get { return nombreDispensador; }
            set { nombreDispensador = value; OnPropertyChanged(); }
        }
        public int NumCasillero
        {
            get { return numCasillero; }
            set { numCasillero = value; OnPropertyChanged(); }
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
        public  double Distancia
        {
            get { return distancia; }
            set { distancia = value; OnPropertyChanged(); }
        }



        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public string Path_Imagen
        {
            get { return path_Imagen; }
            set { path_Imagen = value; }
        }
      
        #endregion
    }
}
