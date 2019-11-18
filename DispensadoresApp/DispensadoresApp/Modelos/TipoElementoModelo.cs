using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class TipoElementoModelo
    {
        //Atributos
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("id")]
        public long id_Tipo_Elemento;
        [JsonProperty("nombre")]
        public string nombre;
        [JsonProperty("idEmpresa")]
        public long id_Empresa;
        [JsonProperty("pathImagen")]
        public string path_Imagen;
        public List<ElementoModelo> elementos;
        public int totalDisponibles;
        #endregion

        //Métodos
        #region Getters/Setters

        public int TotalDisponibles
        {
            get { return totalDisponibles; }
            set { totalDisponibles = value; OnPropertyChanged(); }
        }
        
        public long Id_Tipo_Elemento
        {
            get { return id_Tipo_Elemento; }
            set { id_Tipo_Elemento = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public long Id_Empresa
        {
            get { return id_Empresa; }
            set { id_Empresa = value; }
        }

        public string Path_Imagen
        {
            get { return path_Imagen; }
            set { path_Imagen = value; }
        }

        public List<ElementoModelo> Elementos
        {
            get { return elementos; }
            set { elementos = value; }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
