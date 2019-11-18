using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class ElementoModelo
    {

        #region Constructors
        public ElementoModelo() { }
        public ElementoModelo(string descripcion, string nombre,int id) { 
            this.Descripcion = descripcion;
            this.Nombre = nombre;
            this.id_Elemento = id;

        }

        #endregion
        //Atributo
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("id")]
        public long id_Elemento;
        [JsonProperty("descripcion")]
        public string descripcion;
        [JsonProperty("nombre")]
        public string nombre;
        public string path_Imagen;
        public int estado;
        #endregion


        //Eventos
        public event PropertyChangedEventHandler PropertyChanged;

        //Métodos

        #region Getters/Setters
        public long Id_Elemento
        {
            get { return id_Elemento; }
            set { id_Elemento = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
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
        public void OnPropertyChanged([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }


        #endregion
    }
}
