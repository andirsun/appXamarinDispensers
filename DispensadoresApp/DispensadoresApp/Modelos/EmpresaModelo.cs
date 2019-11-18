using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DispensadoresApp.Modelos
{
    public class EmpresaModelo
    {
        //Atributos
        #region Atributos
        
        [PrimaryKey, AutoIncrement]//Id of DB 
        public int ID { get; set; }
        [JsonProperty("id")]
        public long id_Empresa;
        [JsonProperty("nombre")]
        public string nombre;
        [JsonProperty("tiempoReservaMax")]
        public int tiempo_Reserva_Max;
        public List<DispensadorModelo> dispensadores;
        #endregion

        //Métodos
        #region Getters/Setters
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public int Tiempo_Reserva_Max
        {
            get { return tiempo_Reserva_Max; }
            set { tiempo_Reserva_Max = value; }
        }
        public List<DispensadorModelo> Dispensadores
        {
            get { return dispensadores; }
            set { dispensadores = value; }
        }
        #endregion
    }
}
