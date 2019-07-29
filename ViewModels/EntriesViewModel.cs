using System.Collections.ObjectModel;
using System.Linq;
using wpf_gastosPessoais.Models;
using System.Windows;
using System.Collections.Specialized;
using System.Windows.Input;
using wpf_gastosPessoais.Misc;
using wpf_gastosPessoais.Data;

namespace wpf_gastosPessoais.ViewModels
{
    public class EntriesViewModel : ViewModelBase
    {

        public EntriesViewModel()
        {
            repository = new EntryRepository();
            entries = new TrulyObservableCollection<Entry>(repository.GetAll());
            entries.CollectionChanged += Entries_CollectionChanged;
            entryControls = new ObservableCollection<EntryControlViewModel>();
            foreach (var item in entries)
            {
                AddEntryControlVM(item);
            }

        }

        private EntryRepository                                 repository;
        private ICommand                                        addEntry;
        private TrulyObservableCollection<Entry>                entries;
        private ObservableCollection<EntryControlViewModel>     entryControls;
        public  TrulyObservableCollection<Entry>                AllEntries
        {
            get => entries;
            set
            {
                entries = value;
                OnPropertyChanged("AllEntries");
            }
        }
        public  ObservableCollection<EntryControlViewModel>     AllEntriesControls
        {
            get => entryControls;
            set
            {
                entryControls = value;
                OnPropertyChanged("AllEntryControls");
            }
        }
        public  Visibility                                      FirstEntryVisibility
        {
            get => entries.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        public  Visibility                                      EntryGridVisibility
        {
            get => entries.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        public  ICommand                                        AddEntry
        {
            get
            {
                if (addEntry == null)
                    addEntry = new RelayCommand(AddEntryCommand);
                return addEntry;
            }
            set => addEntry = value;
        }

        private void Entries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                bool removed;
                foreach (var item in e.OldItems)
                {
                    Entry entry = (Entry)item;
                    removed = !entries.Contains(entry);
                    if (removed)
                    {
                        entryControls.Remove(entryControls.FirstOrDefault(x => x.Entry == entry));
                        repository.Delete(entry);
                    }
                }
            }
            if(e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    AddEntryControlVM((Entry)item);
                    repository.Save((Entry)item);
                }
            }
            OnPropertyChanged("FirstEntryVisibility", "EntryGridVisibility", "AllEntries");
        }

        private void AddEntryControlVM(Entry item)
        {
            entryControls.Add(new EntryControlViewModel(ref entries)
            {
                Entry = item
            });
        }

        private void AddEntryCommand(object parameter)
        {
            new WindowHost().ShowDialog(new EditEntryViewModel(this));
        }

    }

}
