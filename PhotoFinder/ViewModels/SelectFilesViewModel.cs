using PhotoFinder.Commands;
using PhotoFinder.Handlers;
using PhotoFinder.Models;
using PhotoFinder.Services;
using PhotoFinder.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PhotoFinder.ViewModels
{
    public class SelectFilesViewModel
    {
        public ICommand RemoveSelectedFoldersCommand { get; }
        public ICommand RemoveSelectedTargetsCommand { get; }
        public ICommand AddPhotoFoldersCommand { get; }
        public ICommand AddTargetsCommand { get; }
        public ICommand AddTargetManuallyCommand { get; set; }
        public ObservableCollection<PhotoFolder> PhotoFolders { get; set; } = new();
        public ObservableCollection<SearchingTarget> Targets { get; set; } = new();
        public SelectFilesViewModel()
        {
            RemoveSelectedFoldersCommand = new RelayCommand<object>(RemoveSelectedFolders);
            RemoveSelectedTargetsCommand = new RelayCommand<object>(RemoveSelectedTargets);
            AddPhotoFoldersCommand = new RelayCommand(_ => AddPhotoFolders());
            AddTargetsCommand = new RelayCommand(_ => AddTargets());
            AddTargetManuallyCommand = new RelayCommand(_ => AddTarget());

            Targets = new();
            PhotoFolders = new();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void RemoveSelectedFolders(object parameter)
        {
            if (parameter is IList<object> selectedItems)
            {
                var itemsToRemove = selectedItems.Cast<PhotoFolder>().ToList();
                foreach (var item in itemsToRemove)
                    PhotoFolders.Remove(item);
            }
        }
        private void RemoveSelectedTargets(object parameter)
        {
            if (parameter is IList<object> selectedItems)
            {
                var itemsToRemove = selectedItems.Cast<SearchingTarget>().ToList();
                foreach (var item in itemsToRemove)
                    Targets.Remove(item);
            }
        }
        private void AddPhotoFolders()
        {
            try
            { PhotoFolderHandler.LoadData(PhotoFolders); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void AddTargets()
        {
            try
            { SHPFileHandler.LoadData(Targets); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void AddTarget()
        {
            AddPointView pointAdder = new();
            pointAdder.ShowDialog();

            if (pointAdder.DialogResult == true)
                Targets.Add(pointAdder.NewTarget);
        }
    }

}
