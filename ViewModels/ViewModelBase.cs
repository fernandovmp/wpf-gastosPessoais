using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_gastosPessoais.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params string[] property)
        {
            if(PropertyChanged != null)
            {
                foreach (var p in property)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(p));
                }
                
            }
        }

        public ViewModelBase CurrentView { get => this; }

    }
}
