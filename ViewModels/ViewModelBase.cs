using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_gastosPessoais.Misc;

namespace wpf_gastosPessoais.ViewModels
{
    public class ViewModelBase : NotifyPropertyChanged
    {
        public ViewModelBase CurrentView { get => this; }
    }
}
