using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using System.Runtime.CompilerServices;

namespace DispensadoresApp.ViewModels
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null) //estaba name=""
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField,value))
            {
                return;
            }
            backingField = value;
            OnPropertyChanged(propertyName);
        }

    }
}
