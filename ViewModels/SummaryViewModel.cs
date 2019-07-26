using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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

        private TrulyObservableCollection<Entry> entries;
        private SeriesCollection spentDistribution;
        private decimal balance;
        private decimal income;
        private decimal spents;
        public  SeriesCollection SpentDistribution
        {
            get => spentDistribution;
            set
            {
                spentDistribution = value;
                OnPropertyChanged("SpentDistribution");
            }
        }
        public TrulyObservableCollection<Entry> Entries
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
        public  string Balance
        {
            get => $"Saldo: R$ {balance.ToString("F2")}";
        }
        public  string Income
        {
            get => $"Receita: R$ {income.ToString("F2")}";
        }
        public  string Spents
        {
            get => $"Despesas: R$ {spents.ToString("F2")}";
        }
        public  Visibility NoneSpentsVisibility
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
            foreach (var entry in Entries)
            {
                balance += entry.SignedValue;
                if(entry.EntryType == EntryType.Credit)
                {
                    income += entry.Value;
                    continue;
                }
                spents += entry.Value;
            }
            OnPropertyChanged("Balance", "Income", "Spents");
        }

    }
}
