using PhotoFinder.Commands;
using PhotoFinder.Services;
using PhotoFinder.Services.Reader;
using PhotoFinder.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoFinder.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        private readonly INavigationService _navigationService;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public ICommand ShowSelectFilesViewCommand { get; }
        public ICommand ShowResultsViewCommand { get; set; }
        public MainViewModel()
        {
            _navigationService = new NavigationService(this);
            CurrentView = new SelectFilesView();

            ShowSelectFilesViewCommand = new RelayCommand(_ => _navigationService.NavigateTo(new SelectFilesView()));
            ShowResultsViewCommand = new RelayCommand(_ => ProcessData());
        }

        private void ProcessData()
        {            
            if (CurrentView is SelectFilesView selectFilesView)
            {
                var viewModel = selectFilesView.ViewModel;
                var photosData = DataReaderService.ReadInput(viewModel.PhotoFolders.ToList());

                IPhotoFinderService photoFinderService = new PhotoFinderService();
                var results = photoFinderService.GetResults(viewModel.Targets.ToList(), photosData);
                ResultsViewModel resultsViewModel = new(results);

                _navigationService.NavigateTo(new ResultsView(resultsViewModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
