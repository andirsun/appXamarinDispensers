using DispensadoresApp.Modelos;
using DispensadoresApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace DispensadoresApp.Servicios.ApiRest
{
    public class DispensadorApiRest
    {
        #region Atributos
        private readonly HttpClient client;
        private List<DispensadorModelo> dispensador;
        private List<ElementoModelo> elementos;
        private List<TipoElementoModelo> tipo_Elementos;
        #endregion

        #region Getters/Setters
        public List<DispensadorModelo> Dispensador
        {
            get { return dispensador; }
            set { dispensador = value; }
        }

        public List<ElementoModelo> Elementos
        {
            get { return elementos; }
            set { elementos = value; }
        }

        public List<TipoElementoModelo> Tipo_Elementos
        {
            get { return tipo_Elementos; }
            set { tipo_Elementos = value; }
        }
        #endregion

        #region Constructor
        public DispensadorApiRest()
        {
            if (client == null) { client = new HttpClient(); }
        }
        #endregion

        #region Listado de Dispensadores por Empresa
        public async Task<List<DispensadorModelo>> Lista_Dispensadores(UsuarioModelo usuario)
        {
            Dispensador = new List<DispensadorModelo>();
            try
            {
                var gm = new GetMaker();
                var url = GlobalSettings.BASE_URL + GlobalSettings.DISPENSADORES_URL;
                string[] contenido = new string[1];
                //contenido[0] = usuario.Id_Empresa.ToString();
                string finalurl = gm.Replace(url, contenido);
                var httpResponse = await client.GetAsync(finalurl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Dispensador = JsonConvert.DeserializeObject<List<DispensadorModelo>>(responseContent);
                }
            }
            catch (Exception e)
            {

            }

            return Dispensador;
        }

        #endregion

        #region Listado de Equipos por Dispensador
        public async Task<List<ElementoModelo>> Lista_Elementos(DispensadorModelo dispensador)
        {
            Elementos = new List<ElementoModelo>();
            try
            {
                var gm = new GetMaker();
                var url = GlobalSettings.BASE_URL + GlobalSettings.EQUIPOS_DISPENSADOR_URL;
                string[] contenido = new string[1];
                contenido[0] = dispensador.Id_Dispensador.ToString();
                string finalurl = gm.Replace(url, contenido);
                var httpResponse = await client.GetAsync(finalurl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Elementos = JsonConvert.DeserializeObject<List<ElementoModelo>>(responseContent);
                }
            }
            catch (Exception e)
            {

            }

            return Elementos;
        }
        #endregion

        #region Busqueda de Tipos de Equipo por Empresa
        public async Task<List<TipoElementoModelo>> Lista_Tipo_Elemento(UsuarioModelo usuario)
        {
            Tipo_Elementos = new List<TipoElementoModelo>();
            try
            {
                var gm = new GetMaker();
                var url = GlobalSettings.BASE_URL + GlobalSettings.BUSQUEDA_TIPO_EQUIPO;
               
                string[] contenido = new string[1];
                //contenido[0] = usuario.Id_Empresa.ToString();
                string finalurl = gm.Replace(url, contenido);
                var httpResponse = await client.GetAsync(finalurl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Tipo_Elementos = JsonConvert.DeserializeObject<List<TipoElementoModelo>>(responseContent);
                }
            }
            catch (Exception e)
            {

            }

            return Tipo_Elementos;
        }

        #endregion

        #region Busqueda de Equipos por Tipo de Equipo
        public async Task<List<ElementoModelo>> Lista_Equipos_Tipo_Equipo(TipoElementoModelo tipo)
        {
            Elementos = new List<ElementoModelo>();
            try
            {
                var gm = new GetMaker();
                var url = GlobalSettings.BASE_URL + GlobalSettings.LISTADO_EQUIPOS_TIPO_EQUIPO;
                string[] contenido = new string[1];
                //contenido[0] = tipo.Id_Tipo_Elemento.ToString();
                string finalurl = gm.Replace(url, contenido);
                var httpResponse = await client.GetAsync(finalurl);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Elementos = JsonConvert.DeserializeObject<List<ElementoModelo>>(responseContent);
                }
            }
            catch (Exception e)
            {

            }

            return Elementos;
        }
        #endregion

    }
}
