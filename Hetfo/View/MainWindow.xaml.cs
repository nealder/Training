using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Hetfo.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ICollectionView CollectionView { get; set; }
        ViewModel.MainViewModel ActualWindow = new ViewModel.MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Open_xml_click_caller(object sender, RoutedEventArgs e)
        {

            ActualWindow.Open_xml_click_handler(sender, e);

            MyList.ItemsSource = ActualWindow.SourceCollection;
        }
    }
}
