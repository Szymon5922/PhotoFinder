using Microsoft.Win32;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhotoFinder.Views
{
    /// <summary>
    /// Interaction logic for AddPointView.xaml
    /// </summary>
    public partial class AddPointView : Window
    {
        public SearchingTarget NewTarget { get; set; }
        public AddPointView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddPoint_Click(object sender, RoutedEventArgs e)
        {
            float x, y, z;
            try
            {
                x = float.Parse(PointX.Text);
                y = float.Parse(PointY.Text);
                z = float.Parse(PointZ.Text);
                Vector3 newPoint = new(x, y, z);

                NewTarget = new(newPoint);

                if (!string.IsNullOrEmpty(ImagePathTextBox.Text))
                    NewTarget.ImageDirectory = ImagePathTextBox.Text;

                this.DialogResult = true;
            }
            catch(Exception ex)
            { MessageBox.Show("Wrong point coordinates format"); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose image",
                Filter = "Images (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            };
            if(openFileDialog.ShowDialog()==true)
                ImagePathTextBox.Text = openFileDialog.FileName;
        }
    }
}
