using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using DispensadoresApp.ViewModels;
using DispensadoresApp.Views;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

namespace DispensadoresApp.Servicios.Navegacion
{
    public class NavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public Task InitializeAsync()
        {
            var propertie = Application.Current.Properties.ContainsKey("token");
            if (propertie)
            {
                return NavigateToAsync<DispensadorViewModel>(); //si esta definido el guardado el token debera ir a la pagina de los dispensadores
            }
            else
            {
                return NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;
            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }
            return Task.FromResult(true);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack
                                               [mainPage.Navigation.NavigationStack.Count - 2]);
            }
            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType);

            if (page is LoginView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        public async Task NavigatePopToAsync(ViewModelBase viewModelBase)
        {
            var viewModelType = viewModelBase.GetType();
            var pageType = GetPageTypeForViewModel(viewModelType, true);
            PopupPage page = Activator.CreateInstance(pageType) as PopupPage;
            page.BindingContext = viewModelBase;
            await PopupNavigation.Instance.PushAsync(page);
        }

        public async Task GoBackPopAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private Type GetPageTypeForViewModel(Type viewModelType, bool isPopUp = false)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            if (isPopUp)
            {
                viewName = viewName + "Pop";
            }
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}",
                                                 viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }
            try
            {
                Page page = Activator.CreateInstance(pageType) as Page;
                return page;
            }
            catch(Exception e)
            {
                var x = e;
                return new Page();
            }
            
            
        }

        public async Task GoBackAsync()
        {
            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                await navigationPage.PopAsync();
            }
        }

    }
}
