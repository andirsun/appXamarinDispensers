using DispensadoresApp.Modelos;
using DispensadoresApp.Models;
using DispensadoresApp.Servicios;
using DispensadoresApp.Servicios.ApiRest;
using DispensadoresApp.Servicios.Rest;
using DispensadoresApp.Servicios.BaseDatos;
using DispensadoresApp.Views;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace DispensadoresApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Atributos 
        private UsuarioModelo usuario;
        public string User { get; set; }
        public string Password { get; set; }
        private ObservableCollection<EmpresaModelo> listaEmpresas;
        private EmpresaModelo empresaSeleccionada;
 
        #endregion

        #region Servicios
        public PostService<UsuarioPostModel> servicioPostLogin { get; set; }
        public GetService<string> servicioGetTipoEquipo { get; set; }
        public GetService<EmpresaModelo> getEmpresa { get; set; }
        public GetService<DispensadorModelo> getDispensadores { get; set; }
        


        #endregion

        #region Commands
        public Command PopUpCommandView { get; set; }
        public Command ReservarCommand { get; set; }
        public Command LoginCommand { get; set; }
        #endregion
        #region Getters/Setters
        public EmpresaModelo EmpresaSeleccionada
        {
            get { return empresaSeleccionada; }
            set { empresaSeleccionada = value; OnPropertyChanged(); }
        }
        public UsuarioModelo Usuario
        {
            get { return usuario; }
            set { usuario = value; OnPropertyChanged(); }
        }
        public ObservableCollection<EmpresaModelo> ListaEmpresas
        {
            get { return listaEmpresas; }
            set { listaEmpresas = value;
                  OnPropertyChanged();
                }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            
            Usuario = new UsuarioModelo();
            LoginCommand = new Command(async () => await Login(), () => true);
            ReservarCommand = new Command(async () => await ClosePopUp(), () => true);
            PopUpCommandView = new Command(async () => await PopUpView(), () => true);

            
             
        }
        public override  async Task InitializeAsync(object navigationData)
        {
            await listarEmpresas();
            
        }

        #endregion
        private async Task listarEmpresas()
        {
            var url = GlobalSettings.BASE_URL + GlobalSettings.LISTAR_EMPRESAS;
            var empresa = new EmpresaModelo();
            var getEmpresa = new GetService<EmpresaModelo>(url);
            Tuple<bool, string> ans = await getEmpresa.SendRequest(empresa);
            if (ans.Item1)
            {
                List<EmpresaModelo> empresas = JsonConvert.DeserializeObject<List<EmpresaModelo>>(ans.Item2);
                ListaEmpresas = new ObservableCollection<EmpresaModelo>(empresas);
                
            }

        }
        
        private async Task Login()
        {
            
            var urlLogin = GlobalSettings.BASE_URL + GlobalSettings.LOGIN_URL;
            var userLogin = new UsuarioPostModel();
            userLogin.UserName = User;
            userLogin.Password = Password;
            userLogin.IdEmpresa = EmpresaSeleccionada.Id_Empresa;
            servicioPostLogin = new PostService<UsuarioPostModel>(urlLogin);
            Tuple<bool, string> ans = await servicioPostLogin.SendRequest(userLogin);//Working
            var response = ans.Item1;
            if (response)
            {
                //Login Sucess
                UsuarioModelo user = JsonConvert.DeserializeObject<UsuarioModelo>(ans.Item2);
                await PersistenceDataAsync("token",user.Api_token);
                await PersistenceDataAsync("UserID",user.Id_Usuario);
                await PersistenceDataAsync("UserName",user.Nombre);
                await PersistenceDataAsync("idEmpresa", userLogin.IdEmpresa);
                await PersistenceDataAsync("urlImagen",EmpresaSeleccionada.PathImagenMapa);
                await NavigationService.NavigateToAsync<DispensadorViewModel>();
                 
                await NavigationService.RemoveLastFromBackStackAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error en sus credenciales", "OK");
                //await PopUpView(); //Desplega mensaje de Logeo fallido
                
            }                      
        }
        public async Task PersistenceDataAsync(string key, object value)//guarda en memoria segura los datos de Logeo
        {
            try
            {
                var stringValue = value.ToString();
                await SecureStorage.SetAsync(key, stringValue);
            }
            catch (Exception)
            {

            }
            Application.Current.Properties[key] = value;
        }

        private async Task ClosePopUp()
        {
            await PopupNavigation.Instance.PopAsync();
        }
        private async Task PopUpView()
        {

            await PopupNavigation.PushAsync(new FailLogin());
        }
    }
}
