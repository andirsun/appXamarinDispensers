using System;
using System.Collections.Generic;
using System.Text;
using DispensadoresApp.ViewModel;
using System.Threading.Tasks;


namespace DispensadoresApp.Servicios.Navegacion
{
    public interface INavigationService
    {

        ViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task GoBackAsync();
        Task GoBackPopAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigatePopToAsync(ViewModelBase viewModelBase);
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();

    }
}
