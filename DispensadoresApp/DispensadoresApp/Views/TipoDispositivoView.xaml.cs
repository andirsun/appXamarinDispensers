﻿using DispensadoresApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DispensadoresApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class TipoDispositivoView : ContentPage
    {
        TipoDispositivoViewModel context = new TipoDispositivoViewModel();
        public TipoDispositivoView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}