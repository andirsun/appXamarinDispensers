using DispensadoresApp.Modelos;
using DispensadoresApp.Models;
using DispensadoresApp.Servicios;
using DispensadoresApp.Servicios.ApiRest;
using DispensadoresApp.Servicios.Rest;
using DispensadoresApp.Servicios.BaseDatos;
using DispensadoresApp.Servicios.Geolocalizacion;
using DispensadoresApp.Servicios.Navegacion;
using DispensadoresApp.Views;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace DispensadoresApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Atributos 
        private UsuarioModelo usuario;
        public List<DispensadorModelo> Dispensadores { get; set; }
        #endregion

        #region Servicios
        private readonly LoginApiRest servicio_Login;
        private readonly DispensadorApiRest servicio_Dispensador;
        private readonly UsuarioDB servicio_BD_Cliente;
        public PostService<UsuarioPostModel> servicioPostLogin;
        public GetService<string> servicioGetTipoEquipo;
        public GetService<EmpresaModelo> getEmpresa;
        public GetService<DispensadorModelo> getDispensadores;
        public GetService<UbicacionModelo> getUbicaciones;

        #endregion

        #region Getters/Setters
        public UsuarioModelo Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            Usuario = new UsuarioModelo();
            #region Inicializar Servicios
            servicio_Login = new LoginApiRest();
            servicio_Dispensador = new DispensadorApiRest();
            servicio_BD_Cliente = new UsuarioDB();
            
            #endregion

            #region Inicializar Commands
            LoginCommand = new Command(async () => await Login(), () => true);
            #endregion
        }
        #endregion

        #region Commands
        public Command LoginCommand { get; set; }
        #endregion


        
        private async Task tipoEquipo()
        {
            var url = GlobalSettings.BASE_URL + GlobalSettings.BUSQUEDA_TIPO_EQUIPO;
            var idEmpresa = "1";
            var urlParams = url + idEmpresa;//id empresa
            var tipoEquipo = new TipoElementoModelo();
            servicioGetTipoEquipo = new GetService<string>(urlParams);
            Tuple<bool, string> ans = await servicioGetTipoEquipo.SendRequest("");
            if (!ans.Item1)
            {
                List<TipoElementoModelo> equipos = JsonConvert.DeserializeObject<List<TipoElementoModelo>>(ans.Item2);
                //////////////Preguntar Por que no funciona Asi serializando en una lista 
           
            }
        }
        private async Task listarEmpresas()
        {
            var url = GlobalSettings.BASE_URL + GlobalSettings.LISTAR_EMPRESAS;
            var empresa = new EmpresaModelo();
            var getEmpresa = new GetService<EmpresaModelo>(url);
            Tuple<bool, string> ans = await getEmpresa.SendRequest(empresa);
            if (!ans.Item1)
            {
                EmpresaModelo empresas = JsonConvert.DeserializeObject<EmpresaModelo>(ans.Item2);
                //////////////Preguntar Por que no funciona Asi serializando en una lista 
                //List<EmpresaModelo> empresas = JsonConvert.DeserializeObject<EmpresaModelo>(ans.Item2);
            }

        }
        private async Task listarDispensadores()
        {
            var param1 = "1";
            var param2 = "2";
            var url = GlobalSettings.BASE_URL + GlobalSettings.DISPENSADORES_URL+param1+param2;
            var dispensador = new DispensadorModelo();
            var getDispensador = new GetService<DispensadorModelo>(url);
            Tuple<bool, string> ans = await getDispensador.SendRequest(dispensador);
            if (!ans.Item1)
            {
                DispensadorModelo dispensadores = JsonConvert.DeserializeObject<DispensadorModelo>(ans.Item2);
                //////////////Preguntar Por que no funciona Asi serializando en una lista 
            }

        }
        private async Task tipoEquipoDisponiblexUSER()
        {
            var userId = "2";//User id
            var url = GlobalSettings.BASE_URL;
            var parameters = GlobalSettings.TIPO_EQUIPO_DISPONIBLE_USUARIO + userId;
            var tipoEquipo = new TipoElementoModelo();
            servicioGetTipoEquipo = new GetService<string>(url);
            Tuple<bool, string> ans = await servicioGetTipoEquipo.SendRequest(parameters);
            if (!ans.Item1)
            {
                TipoElementoModelo equipos = JsonConvert.DeserializeObject<TipoElementoModelo>(ans.Item2);
                //////////////Preguntar Por que no funciona Asi serializando en una lista 

            }

        }
        private async Task ubicacionesCasilleros()
        {
            var idDispensador = "1";
            var url = GlobalSettings.BASE_URL + GlobalSettings.CASILLAS_DISPENSADOR + idDispensador;
            var ubicacion = new UbicacionModelo();
            getUbicaciones = new GetService<UbicacionModelo>(url);
            Tuple<bool, string> ans = await getUbicaciones.SendRequest(ubicacion);
            if (!ans.Item1)
            {
                UbicacionModelo ubicaciones = JsonConvert.DeserializeObject<UbicacionModelo>(ans.Item2);
                //////////////Preguntar Por que no funciona Asi serializando en una lista 

            }

        }

        private async Task tiempoMaxReserva() /*PREGUNTAR SI SE CREA UN MODELO APARTE SOLO PARA tiempo maximo de reserva*/
        {
            var idDispensador = "1";
            var url = GlobalSettings.BASE_URL + GlobalSettings.TIEMPO_MAX_RESERVA + idDispensador;
            var ubicacion = new UbicacionModelo();
            getUbicaciones = new GetService<UbicacionModelo>(url);
            Tuple<bool, string> ans = await getUbicaciones.SendRequest(ubicacion);
            if (!ans.Item1)
            {
                UbicacionModelo ubicaciones = JsonConvert.DeserializeObject<UbicacionModelo>(ans.Item2);
                //////////////Preguntar Por que no funciona Asi serializando en una lista 

            }

        }
        private async Task Login()
        {
            
            var urlLogin = GlobalSettings.BASE_URL + GlobalSettings.LOGIN_URL;
            var userLogin = new UsuarioPostModel();
            userLogin.UserName = "Pepe";
            userLogin.Password = "1234";
            userLogin.IdEmpresa = 1;
            servicioPostLogin = new PostService<UsuarioPostModel>(urlLogin);
            Tuple<bool, string> ans = await servicioPostLogin.SendRequest(userLogin);//Working
            if (!ans.Item1)
            {
                UsuarioModelo user = JsonConvert.DeserializeObject<UsuarioModelo>(ans.Item2);
                Application.Current.Properties["token"] = "Bearer "+ user.Api_token;
                Application.Current.Properties["UserID"] ="ID " + user.ID;
                Application.Current.Properties["idEmpresa"] = "ID ";//GUARDAR DESPUES DE QUE EL LOGIN SEA EXIROSO

            }


            var r = 1;

            //Application.Current.MainPage = new NavigationPage(new DispensadorView(Usuario)); ///Redirecting to another page after login
            await NavigationService.NavigateToAsync<TipoDispositivoViewModel>();
            await NavigationService.RemoveLastFromBackStackAsync();

            





            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var nose = new Geolocalizacion(location.Latitude, location.Longitude);
                    var distancia = nose.distanceBetween(3.381991, -76.518472);
                    var distancia2 = nose.Calcular();


                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception

            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            //var usuarioAgregado = await servicio_BD_Cliente.AgregarUsuario(Usuario);
            //var usuarioLogueado = await servicio_Login.LoginUsuario(Usuario);
           
            
            //var navigationStack = navigation.NavigationStack;//Pendiente por preguntar
            //navigation.RemovePage(navigationStack[navigationStack.Count - 2]); //Pendiente por preguntar
        }
    }
}
