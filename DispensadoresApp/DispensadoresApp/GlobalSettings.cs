using System;
using System.Collections.Generic;
using System.Text;

namespace DispensadoresApp
{
    public class GlobalSettings
    {
        public static readonly string BASE_URL = "https://pruebasreplica3.javerianacali.edu.co/ServiciosDispensadores";
        public static readonly string TOKEN = "";
        public static readonly string LOGIN_URL = "/login";
        public static readonly string LISTAR_EMPRESAS = "/empresas";
        public static readonly string TIEMPO_MAX_RESERVA = "/tiempo/";
        public static readonly string CASILLAS_DISPENSADOR = "/ubicaciones/";
        public static readonly string RESERVAR_URL = "/reservar"; //Reservar equipo
        public static readonly string DISPENSADORES_URL = "/dispensadores/"; //Listado de dispensadores dado el ID de la empresa
        public static readonly string EQUIPOS_DISPENSADOR_URL = "/equiposDispensador/"; // Listado de equipos por dispensador
        public static readonly string BUSQUEDA_TIPO_EQUIPO = "/tiposEquipos/"; // Listado de tipo de equipo por ID empresa
        public static readonly string TIPO_EQUIPO_DISPONIBLE_USUARIO = "/tiposDisponibles/"; //Que tipos y el numero de equipos tiene disponibles el use
        public static readonly string LISTADO_EQUIPOS_TIPO_EQUIPO = "/equipos/"; //Listado de equipos por tipo de equipo
        
    }
}
