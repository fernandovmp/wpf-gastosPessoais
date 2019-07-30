using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using wpf_gastosPessoais.Models;
using System.Windows;
using wpf_gastosPessoais.Misc;

namespace wpf_gastosPessoais.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {

        public SummaryViewModel()
        {
            
        }

        private TrulyObservableCollection<Entry>    entries;
        private SeriesCollection                    spentDistribution;
        private decimal                             balance;
        private decimal                             income;
        private decimal                             spents;
        public  SeriesCollection                    SpentDistribution
        {
            get => spentDistribution;
            set
            {
                spentDistribution = value;
                OnPropertyChanged("SpentDistribution");
            }
        }
        public  TrulyObservableCollection<Entry>    Entries
        {
            get => entries;
            set
            {
                entries = value;
                UpdateInfo();
                UpdateBalance();
                OnPropertyChanged("Entries");
            }
        }
        public  decimal                             Balance
        {
            get => balance;
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        public  decimal                             Income
        {
            get => income;
            set
            {
                income = value;
                OnPropertyChanged("Income");
            }
        }
        public  decimal                             Spents
        {
            get => spents;
            set
            {
                spents = value;
                OnPropertyChanged("Spents");
            }
        }
        public  Visibility                          NoneSpentsVisibility
        {
            get
            {
                return SpentDistribution.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdateInfo()
        {
            Dictionary<string, decimal> groups = new Dictionary<string, decimal>();
            var x = from entry in Entries
                    where entry.EntryType == EntryType.Debit
                    select entry;
            foreach (var entry in x)
            {
                if(!groups.ContainsKey(entry.Group))
                {
                    groups.Add(entry.Group, entry.Value);
                    continue;
                }
                groups[entry.Group] += entry.Value;
            }
            SeriesCollection series = new SeriesCollection();
            foreach (var group in groups)
            {
                series.Add(new PieSeries
                {
                    Values = new ChartValues<decimal> { group.Value },
                    Title = group.Key,
                    DataLabels = true,
                    LabelPoint = chartPoint => $"R$ {group.Value}"
                });

            }
            SpentDistribution = series;
            OnPropertyChanged("NoneSpentsVisibility");
        }

        private void UpdateBalance()
        {
            Balance = Income = Spents = 0;
            foreach (var entry in Entries)
            {
                Balance += entry.SignedValue;
                if(entry.EntryType == EntryType.Credit)
                {
                    Income += entry.Value;
                    continue;
                }
                Spents += entry.Value;
            }
        }

    }
}
