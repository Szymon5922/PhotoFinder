using PhotoFinder.Models;
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
using System.Windows.Shapes;

namespace PhotoFinder.Views
{
    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView : Window
    {
        public OptionsView()
        {
            InitializeComponent();
        }
        public void Close_Click(object sender, EventArgs e)
        {
            ExportParams.GenerateCross = GenerateCross.IsChecked ?? false;
            ExportParams.GeneratePhotoname = GeneratePhotoname.IsChecked ?? false;
            this.Close();
        }
    }
}
