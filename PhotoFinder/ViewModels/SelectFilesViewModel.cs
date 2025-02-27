using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.ViewModels
{
    public class SelectFilesViewModel
    {
        public ObservableCollection<PhotoFolder> PhotoFolders { get; set; }
        public ObservableCollection<SearchingTarget> Targets { get; set; }
        public SelectFilesViewModel()
        {
            Targets = new();
            PhotoFolders = new();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
