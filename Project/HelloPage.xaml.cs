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

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для HelloPage.xaml
    /// </summary>
    public partial class HelloPage : Page
    {
        int type;
        public HelloPage(int type)
        {
            this.type = type;
            InitializeComponent();

        }

        private void ToClient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(type));
        }

        private void ToAuto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auto(type));
        }

        private void ToProcat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Procat(type));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case 1:
                        ToClient.Visibility = Visibility.Visible;
                        ToAuto.Visibility = Visibility.Visible;
                        ToProcat.Visibility = Visibility.Visible;
                    break;

                case 2: 
               
                ToClient.Visibility = Visibility.Collapsed;
                ToAuto.Visibility = Visibility.Visible;
                ToProcat.Visibility = Visibility.Visible;
                    break;
                case 3:
                    ToClient.Visibility = Visibility.Collapsed;
                    ToAuto.Visibility = Visibility.Visible;
                    ToProcat.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void ReLoging_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
