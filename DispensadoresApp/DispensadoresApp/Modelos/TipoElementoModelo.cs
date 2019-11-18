using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;
using DispensadoresApp.ViewModels;

namespace DispensadoresApp.Modelos
{
    public class TipoElementoModelo : ViewModelBase
    {
        //Atributos
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("id")]
        public int id;
        [JsonProperty("nombre")]
        public string nombre;
        [JsonProperty("pathImagen")]
        public string pathImagen;
        [JsonProperty("cantidad")]
        public int cantidad;
        #endregion

        
       
        
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(); }
        }
        public string PathImagen
        {
            get { return pathImagen; }
            set { pathImagen = value; OnPropertyChanged(); }
        }
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; OnPropertyChanged(); }
        }

        
    }
}
