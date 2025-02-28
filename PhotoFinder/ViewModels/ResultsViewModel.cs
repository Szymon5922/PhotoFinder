using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Windows.Input;
using PhotoFinder.Commands;
using System.Diagnostics;
using Microsoft.Win32;
using PhotoFinder.Helpers;
using PhotoFinder.Services;
using System.Windows;

namespace PhotoFinder.ViewModels
{
    public class ResultsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Result> Results { get; set; } = new();
        public ICommand SelectFoundPhotoCommand { get; }
        public ICommand OpenPhotoCommand { get; }
        public ICommand GenerateImagesCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private Result? _selectedResult;
        public Result? SelectedResult
        {
            get => _selectedResult;
            set
            {
                _selectedResult = value;
                OnPropertyChanged(nameof(SelectedResult));
                FoundPhotos = new ObservableCollection<FoundPhoto>(SelectedResult?.FoundPhotos ?? new List<FoundPhoto>());
                OnPropertyChanged(nameof(SelectedImage)); // refresh main photo
                SelectedFoundPhoto = null;
            }
        }
        public ImageSource? SelectedImage
        {
            get
            {
                if (SelectedResult?.Target?.ImageDirectory is string path && File.Exists(path))
                {
                    return new BitmapImage(new Uri(path));
                }
                return null;
            }
        }
        private ObservableCollection<FoundPhoto> _foundPhotos = new();
        public ObservableCollection<FoundPhoto> FoundPhotos
        {
            get => _foundPhotos;
            set
            {
                _foundPhotos = value;
                OnPropertyChanged(nameof(FoundPhotos));
            }
        }

        private FoundPhoto? _selectedFoundPhoto;
        public FoundPhoto? SelectedFoundPhoto
        {
            get => _selectedFoundPhoto;
            set
            {
                _selectedFoundPhoto = value;
                OnPropertyChanged(nameof(SelectedFoundPhoto));

                if (value != null)
                    LoadFoundPhoto(value.FilePath);
                else
                    _selectedFoundPhotoImage = null;
                OnPropertyChanged(nameof(SelectedFoundPhotoImage));
            }
        }
        private ImageSource? _selectedFoundPhotoImage;
        public ImageSource? SelectedFoundPhotoImage => _selectedFoundPhotoImage;
        private void LoadFoundPhoto(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                if (ExportParams.GenerateCross)
                    _selectedFoundPhotoImage = ImagesHelper.GetImageWithCross(SelectedFoundPhoto);
                else
                    _selectedFoundPhotoImage = new BitmapImage(new Uri(path));
            }
            else
                _selectedFoundPhotoImage = null;
        }
        private bool _isGenerating;
        public bool IsGenerating
        {
            get => _isGenerating;
            set
            {
                _isGenerating = value;
                OnPropertyChanged(nameof(IsGenerating));
            }
        }
        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public ResultsViewModel(List<Result> results)
        {
            results.ForEach(x => Results.Add(x));

            SelectFoundPhotoCommand = new RelayCommand<FoundPhoto>(SelectFoundPhoto);
            OpenPhotoCommand = new RelayCommand(_ => OpenPhoto());
            GenerateImagesCommand = new AsyncRelayCommand(GenerateImagesAsync);
        }

        private void SelectFoundPhoto(FoundPhoto? foundPhoto)
        {

            if (foundPhoto == null) return;

            FoundPhotos.First(p => p.IsSelected).IsSelected = false;

            SelectedFoundPhoto = foundPhoto;
            SelectedFoundPhoto.IsSelected = true;
        }
        private void OpenPhoto()
        {
            if (!string.IsNullOrEmpty(SelectedFoundPhoto.FilePath))
                Process.Start(new ProcessStartInfo(SelectedFoundPhoto.FilePath) { UseShellExecute = true });
        }
        private async Task GenerateImagesAsync()
        {
            var folderDialog = new OpenFolderDialog();

            if (folderDialog.ShowDialog() == true)
            {
                var folderName = folderDialog.FolderName;

                Progress = 0;
                IsGenerating = true;
                var progress = new Progress<double>(value => Progress = value);

                ImageGeneratorService imageGeneratorService = new ImageGeneratorService(Results.ToList(), folderName);

                await Task.Run(() => imageGeneratorService.GenerateImages(progress));

                IsGenerating = false;
                MessageBox.Show("Generating complete");
            }
        }
    }
}

