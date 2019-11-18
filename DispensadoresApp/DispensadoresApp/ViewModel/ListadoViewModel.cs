using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.Modelos;
using DispensadoresApp.Servicios;
using DispensadoresApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DispensadoresApp.ViewModels
{
    public class ListadoViewModel : ViewModelBase
    {
        //Atributos

        public TipoElementoModelo ElementoSeleccionado { get; set; }
        private ObservableCollection<ElementoModelo> elementos; //Estos son los elementos que se van a desplegar en la tabla
        private ElementoModelo elementoTap;//Este es el elemento al que el usuario le dio click
        private string pathImagen;
        private string nombreDispositivo;
        public ListadoViewModel()
        {
            InicializarCommands();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await listarElementos();
            if (navigationData != null)
            {
                ElementoSeleccionado = navigationData as TipoElementoModelo;

                PathImagen = ElementoSeleccionado.PathImagen; // para mostrar la imagen 
                NombreDispositivo = ElementoSeleccionado.Nombre; //para mostrar el nombre arriba de la tabla

            }
            else
            {
                ElementoSeleccionado = new TipoElementoModelo();

            }


        }

        #region Getters y Setters
        public ObservableCollection<ElementoModelo> Elementos
        {
            get { return elementos; }
            set
            {
                elementos = value;
                OnPropertyChanged();
            }
        }
        public ElementoModelo ElementoTap
        {
            get { return elementoTap; }
            set
            {
                elementoTap = value;
                OnPropertyChanged();
            }
        }
        public string PathImagen
        {
            get { return pathImagen; }
            set
            {
                pathImagen = value;
                OnPropertyChanged();
            }
        }
        public string NombreDispositivo
        {
            get { return nombreDispositivo; }
            set
            {
                nombreDispositivo = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public Command ElementoTapp { get; set; }
        public Command CommandDetalleDispositivoView { get; set; }

        #endregion

        public void InicializarCommands()
        {
            CommandDetalleDispositivoView = new Command<ElementoModelo>(async (ElementoTap) => await DetalleDispositivo(ElementoTap), (ElementoTap) => true);
            ElementoTapp = new Command(async () => await elementoSelec(), () => true);
        }




        private async Task listarElementos()
        {
            var idEmpresa = Application.Current.Properties["idEmpresa"];
            var idUsuario = Application.Current.Properties["UserID"];
            var url = GlobalSettings.BASE_URL + GlobalSettings.ELEMENTOS + idEmpresa + '/' + idUsuario;
            var elemento = new ElementoModelo();
            var getElementos = new GetService<ElementoModelo>(url);
            Tuple<bool, string> ans = await getElementos.SendRequest(elemento);
            if (ans.Item1)
            {
                List<ElementoModelo> listaElementos = JsonConvert.DeserializeObject<List<ElementoModelo>>(ans.Item2);
                try
                {
                    var position = await Geolocation.GetLastKnownLocationAsync();//gt current location

                    if (position != null)
                    {
                        var locator = CrossGeolocator.Current;
                        locator.DesiredAccuracy = 50;

                        Location locacionUsuario = new Location(position.Latitude, position.Longitude);
                        var numDispositivos = listaElementos.Count;

                        for (int i = 0; i < numDispositivos; i++)
                        {
                            Location dispositivoLocacion = new Location(listaElementos[i].Latitud, listaElementos[i].Longitud);
                            listaElementos[i].Distancia = Math.Round(Location.CalculateDistance(dispositivoLocacion, locacionUsuario, DistanceUnits.Kilometers), 1);

                        }
                        listaElementos = listaElementos.OrderBy(item => item.Distancia).ToList<ElementoModelo>();
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Tu telefono no tiene gps", "OK");
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No tienes encendido el gps", "OK");
                }
                catch (PermissionException pEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debes conceder los permisos para acceder a tu ubicacion", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No pudimos detectar tu ubicacion", "OK");
                }
                Elementos = new ObservableCollection<ElementoModelo>(listaElementos);//displego la lista de dispensadores en la vista
            }

        }

        
            private async Task elementoSelec() //El navigation object navigation data es la unica forma de pasar parametros con ese sistma de navegacion
        {
            

            await NavigationService.NavigateToAsync<DetalleDispositivoViewModel>();

        }

        private async Task DetalleDispositivo(ElementoModelo item) //El navigation object navigation data es la unica forma de pasar parametros con ese sistma de navegacion
        {
            item = ElementoTap as ElementoModelo;

            await NavigationService.NavigateToAsync<DetalleDispositivoViewModel>(item);

        }
    }
        
}
