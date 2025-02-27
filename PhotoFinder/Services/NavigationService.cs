using PhotoFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services
{
    public class NavigationService : INavigationService
    {
        private readonly MainViewModel _mainViewModel;
        public NavigationService(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
        public void NavigateTo(object view)
        {
            _mainViewModel.CurrentView = view;
        }
    }
}
