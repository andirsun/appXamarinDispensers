using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class ReservaModelo
    {
        public ReservaModelo()//constructor 
        {

        }
        //Atributos
        #region Atributos
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        public long id_Reserva;
        [JsonProperty("idUsuario")]
        public long id_Usuario;
        [JsonProperty("idEmpresas")]
        public long id_Empresa;
        #endregion

        #region Getters/Setters
        public long Id_Reserva
        {
            get { return id_Reserva; }
            set { id_Reserva = value; }
        }
        public long Id_Usuario
        {
            get { return id_Usuario; }
            set { id_Usuario = value; }
        }
        public long Id_Empresa
        {
            get { return id_Empresa; }
            set { id_Empresa = value; }
        }
        #endregion
    }
}
