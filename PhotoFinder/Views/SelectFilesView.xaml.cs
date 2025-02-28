using PhotoFinder.Handlers;
using PhotoFinder.Helpers;
using PhotoFinder.Models;
using PhotoFinder.Services;
using PhotoFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private void Targets_Drop(object sender, DragEventArgs e)
        {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var path in paths)
            {
                    foreach (var target in SHPFileHandler.ReadFolder(path))
                        ViewModel.Targets.Add(target);
            }
        }

        private void PhotoFolders_Drop(object sender, DragEventArgs e)
        {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var path in paths)
            {
                if (Directory.Exists(path) && !ViewModel.PhotoFolders.Any(f => f.Directory == path))
                    try { ViewModel.PhotoFolders.Add(PhotoFolderHandler.ReadFolder(path)); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void PhotoFolders_DragEnter(object sender, DragEventArgs e)
        {
            FilesHelper.AllowIfFolder(e);
        }
        private void Targets_DragEnter(object sender, DragEventArgs e)
        {
            FilesHelper.AllowwIfSHP(e);
        }

    }
}
