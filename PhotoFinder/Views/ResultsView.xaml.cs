using Microsoft.Win32;
using PhotoFinder.Services;
using PhotoFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoFinder.Views
{
    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        private readonly ResultsViewModel _viewModel;
        private readonly INavigationService _navigationService;
        public ResultsView(ResultsViewModel viewModel, INavigationService navigationService)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _navigationService = navigationService;
            DataContext = _viewModel;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new SelectFilesView());
        }
    }
}
