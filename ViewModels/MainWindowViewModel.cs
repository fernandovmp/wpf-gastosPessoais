using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;
using System.ComponentModel;

namespace wpf_gastosPessoais.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            EntriesViewModel = new EntriesViewModel();
            EntriesViewModel.PropertyChanged += EntriesViewModel_PropertyChanged;
            SummaryViewModel = new SummaryViewModel
            {
                Entries = EntriesViewModel.AllEntries
            };
        }

        public EntriesViewModel EntriesViewModel { get; set; }
        public SummaryViewModel SummaryViewModel { get; set; }

        private void EntriesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "AllEntries")
                SummaryViewModel.Entries = EntriesViewModel.AllEntries;
        }

    }
}
