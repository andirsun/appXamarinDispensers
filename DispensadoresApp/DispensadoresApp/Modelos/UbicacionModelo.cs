using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class UbicacionModelo
    {
        public UbicacionModelo()//Constructor
        {

        }
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        public long id_Ubicacion;
        public long id_Elemento;
        public int estado;
        public string descripcion;
        public string nombre;
        #endregion

        #region Getters/Setters
        public long Id_Ubicacion
        {
            get { return id_Ubicacion; }
            set { id_Ubicacion = value; }
        }

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion  = value; }
        }
        public long Id_Elemento

        {
            get { return id_Elemento; }
            set { id_Elemento = value; }
        }
        
        #endregion
    }
}
