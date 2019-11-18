using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DispensadoresApp.Modelos;
using DispensadoresApp.Servicios;
using DispensadoresApp.Servicios.Rest;
using DispensadoresApp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DispensadoresApp.ViewModels
{
    public class DetalleDispositivoViewModel : ViewModelBase
    {

        private ElementoModelo elemento;
        private int id;
        private string tiempo;
        private string nombreDispensador;
        private double distancia;
        private string nombreElemento;
        private string descripcion;
        private int numCasillero;
        public DetalleDispositivoViewModel()
        {
            InicializarCommands();
        }

        #region Commands
        public Command ReservarCommand { get; set; }
        

        #endregion
        public void InicializarCommands()
        {


            ReservarCommand = new Command<ReservaModelo>(async (item) => await reservar(item), (item) => true); //commanda cuando hay que enviar data

        }

        #region Getters and setters
        public ElementoModelo Elemento
        {
            get { return elemento; }
            set
            {
                elemento = value;
                OnPropertyChanged();
            }
        }
        public string Tiempo
        {
            get { return tiempo; }
            set
            {
                tiempo = value;
                OnPropertyChanged();
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string NombreDispensador
        {
            get { return nombreDispensador; }
            set
            {
                nombreDispensador = value;
                OnPropertyChanged();
            }
        }
        public double Distancia
        {
            get { return distancia; }
            set
            {
                distancia = value;
                OnPropertyChanged();
            }
        }
        public string NombreElemento
        {
            get { return nombreElemento; }
            set
            {
                nombreElemento = value;
                OnPropertyChanged();
            }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                OnPropertyChanged();
            }
        }
        public int NumCasillero
        {
            get { return numCasillero; }
            set
            {
                numCasillero = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public override async Task InitializeAsync(object navigationData)
        {
            // await listarElementos();
            if (navigationData != null)
            {
                Elemento = navigationData as ElementoModelo; //en el elemento estan todas las propiedades que se quieran desplegar en la vista

                NombreDispensador = Elemento.NombreDispensador;
                Distancia = Elemento.Distancia;
                NombreElemento = Elemento.NombreElemento;
                Descripcion = Elemento.Descripcion;
                NumCasillero = Elemento.NumCasillero;
                Id = Elemento.Id;


            }
            else
            {
                Elemento = new ElementoModelo();

            }
            var idEmpresa = Application.Current.Properties["idEmpresa"] as string; ;
            var url = GlobalSettings.BASE_URL + GlobalSettings.TIEMPO_MAX_RESERVA + idEmpresa;
            var empresa = new EmpresaModelo();
            var getUbicaciones = new GetService<EmpresaModelo>(url);
            Tuple<bool, string> ans = await getUbicaciones.SendRequest(empresa);
            if (ans.Item1)
            {
                EmpresaModelo tiempo = JsonConvert.DeserializeObject<EmpresaModelo>(ans.Item2);
                Tiempo = tiempo.TiempoMaxReserva;
            }



        }

        private async Task reservar(ReservaModelo item)
        {
            /*FALTA QUE ARREGLEN EL SERVICIO*/
            var usuario = Application.Current.Properties["UserID"].ToString();
            var id = int.Parse(usuario);


            var url = GlobalSettings.BASE_URL + GlobalSettings.TIEMPO_MAX_RESERVA;
            var reserva = new ReservaModelo();
            reserva.idElemento = Elemento.Id;
            reserva.idUbicacion = Elemento.IdUbicacion;
            reserva.idUsuario = id;
            
            var getUbicaciones = new PostService<ReservaModelo>(url);
            Tuple<bool, string> ans = await getUbicaciones.SendRequest(reserva);
            if (ans.Item1)
            {
                //reserva hecha
            }

            //await NavigationService.NavigateToAsync<ListadoViewModel>(item);
        }

    }
}
