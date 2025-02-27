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

namespace PhotoFinder.ViewModels
{
    public class ResultsViewModel : INotifyPropertyChanged
    {
        private Result _prevResultSelected;
        private FoundPhoto _prevFoundPhotoSelected;
        public ObservableCollection<Result> Results { get; set; } = new();
        public ICommand SelectFoundPhotoCommand { get; }

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
                OnPropertyChanged(nameof(SelectedFoundPhotoImage));
            }
        }

        public ImageSource? SelectedFoundPhotoImage
        {
            get
            {
                if (SelectedFoundPhoto?.FilePath is string path && File.Exists(path))
                {
                    return new BitmapImage(new Uri(path));
                }
                return null;
            }
        }
        public ResultsViewModel(List<Result> results)
        {
            results.ForEach(x => Results.Add(x));

            SelectFoundPhotoCommand = new RelayCommand<FoundPhoto>(SelectFoundPhoto);
        }

        private void SelectFoundPhoto(FoundPhoto? foundPhoto)
        {

            if (foundPhoto == null) return;

            

            SelectedFoundPhoto = foundPhoto;
            SelectedFoundPhoto.IsSelected = true;
        }
    }
}
