using DispensadoresApp.Servicios.BaseDatos;
using DispensadoresApp.Views;
using DLToolkit.Forms.Controls;
using DispensadoresApp.ViewModels;
using DispensadoresApp.Servicios.Navegacion;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DLToolkit.Forms.Controls;
using System.IO;
using DispensadoresApp.Servicios.BaseDatos;

namespace DispensadoresApp
{
    public partial class App : Application
    {
        private static DataBase db;
        static INavigationService navigationService;
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            MainPage = new NavigationPage(new LoginView())
            {
                BarBackgroundColor = Color.FromHex("#153249")
            };
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
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
