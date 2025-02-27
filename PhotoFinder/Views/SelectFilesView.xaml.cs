using PhotoFinder.Handlers;
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
    /// Interaction logic for SelectFilesView.xaml
    /// </summary>
    public partial class SelectFilesView : UserControl
    {
        public readonly SelectFilesViewModel ViewModel;
        public SelectFilesView()
        {
            InitializeComponent();
            ViewModel = new SelectFilesViewModel();
            DataContext = ViewModel;
        }
        private void AddPhotoFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            { PhotoFolderHandler.LoadData(ViewModel.PhotoFolders); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void AddTargetFromShp_Click(object sender, RoutedEventArgs e)
        {
            try
            { SHPFolderHandler.LoadData(ViewModel.Targets); }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
