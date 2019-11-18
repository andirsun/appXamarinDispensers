using DispensadoresApp.Servicios.BaseDatos;
using DLToolkit.Forms.Controls;
using DispensadoresApp.Servicios.Navegacion;
using DispensadoresApp.Modelos;
using System;
using Xamarin.Forms;
using System.IO;
using DispensadoresApp.Views;
using DispensadoresApp.Servicios.Handlers;

namespace DispensadoresApp
{
    public partial class App : Application
    {
        private static DataBase db;
        static INavigationService navigationService;
        Models.UsuarioModelo user;
        static LoadDataHandler LoadData;
        public App()
        {

            InitializeComponent();
            FlowListView.Init();
            LoadData = new LoadDataHandler();
        }

        public static DataBase dataBase
        {
            get
            {
                if (dataBase == null)
                {
                    db = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DispensadoresApp.db3"));
                }
                return db;
            }
        }
        public static INavigationService NavigationService
        {
            get
            {
                if (navigationService == null)
                {
                    navigationService = new NavigationService();
                }
                return navigationService;
            }
        }
        
        private void InitNavigation()
        {
            NavigationService.InitializeAsync();
        }
        protected override async void OnStart()
        {
            await LoadData.LoadData("token");
            await LoadData.LoadData("UserID");
            await LoadData.LoadData("UserName");
            await LoadData.LoadData("idEmpresa");
            await LoadData.LoadData("urlImagen");

            InitNavigation();

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override async void OnResume()
        {
            await LoadData.LoadData("token");
            await LoadData.LoadData("UserID");
            await LoadData.LoadData("UserName");
            await LoadData.LoadData("idEmpresa");
            await LoadData.LoadData("urlImagen");

        }
    }
}
