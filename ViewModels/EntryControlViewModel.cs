﻿using Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using wpf_gastosPessoais;
using wpf_gastosPessoais.Models;

namespace wpf_gastosPessoais.ViewModels
{
    public class EntryControlViewModel : ViewModelBase
    {

        public  EntryControlViewModel(ref ObservableCollection<Entry> collection)
        {
            Entries = collection;
        }
        
        private ObservableCollection<Entry> Entries { get; set; }
        private ICommand        deleteEntry;
        private ICommand        editEntry;
        public  Entry           Entry { get; set; }
        public  string          EntryName { get => Entry.Name; }
        public  string          EntryValue { get => $"R$ {Entry.Value.ToString("F2")}"; }
        public  string          EntryGroup { get => Entry.Group; }
        public  string          TypeOfEntry { get => Entry.EntryType.GetString(); }
        public  Brush           ValueForeground
        {
            get => Entry.EntryType == EntryType.Credit ? Brushes.LimeGreen : Brushes.Red;
        }
        public  ICommand        DeleteEntry
        {
            get
            {
                if(deleteEntry == null)
                    deleteEntry = new RelayCommand(DeleteEntryCommand);
                return deleteEntry;
            }
            set
            {
                deleteEntry = value;
            }
        }
        public  ICommand        EditEntry
        {
            get
            {
                if (editEntry == null)
                    editEntry = new RelayCommand(EditEntryCommand);
                return editEntry;
            }
            set
            {
                editEntry = value;
            }
        }

        private void DeleteEntryCommand(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Tem certeza que deseja apagar esse lançamento?",
                $"Excluir {Entry.Name}", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Entries.Remove(Entry);
            }
        }

        private void EditEntryCommand(object parameter)
        {
            WindowHostView window = new WindowHostView
            {
                DataContext = new EditEntryViewModel(this)
            };
            window.ShowDialog();
        }

        public void SaveEdit()
        {
            DatabaseManager.Update(Entry, $"Id = {Entry.Id}");
        }

        public void NotifyEdit()
        {
            OnPropertyChanged("EntryName", "EntryValue", "EntryGroup");
        }

    }
}