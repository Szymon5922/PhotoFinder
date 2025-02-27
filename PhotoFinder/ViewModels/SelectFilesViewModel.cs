using PhotoFinder.Commands;
using PhotoFinder.Models;
using PhotoFinder.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoFinder.ViewModels
{
    public class SelectFilesViewModel
    {
        public ObservableCollection<PhotoFolder> PhotoFolders { get; set; } = new();
        public ObservableCollection<SearchingTarget> Targets { get; set; } = new();
        public ICommand AddPointCommand { get; }
        public SelectFilesViewModel()
        {
            AddPointCommand = new RelayCommand(_ =>  AddPoint());
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void AddPoint()
        {
            AddPointView pointAdder = new();
            pointAdder.ShowDialog();

            if(pointAdder.DialogResult==true)
                Targets.Add(pointAdder.NewTarget);
        }
    }
}
