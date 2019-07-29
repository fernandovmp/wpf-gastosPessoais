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
            EntriesViewModel.AllEntries.ItemChanged += AllEntries_ItemChanged;
            GoalsViewModel = new GoalsViewModel();
        }

        public EntriesViewModel EntriesViewModel { get; set; }
        public SummaryViewModel SummaryViewModel { get; set; }
        public GoalsViewModel   GoalsViewModel { get; set; }

        private void EntriesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "AllEntries")
                SummaryViewModel.Entries = EntriesViewModel.AllEntries;
        }

        private void AllEntries_ItemChanged(object sender, Misc.ItemChangedEventArgs<Models.Entry> e)
        {
            SummaryViewModel.Entries = EntriesViewModel.AllEntries;
        }

    }
}
