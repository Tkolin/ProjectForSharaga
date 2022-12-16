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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        int type;
        public Client(int type)
        {
            this.type = type;
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditClient(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var ClientForRemoving = DGridProject.SelectedItems.Cast<PRClient>().ToList();

            if (MessageBox.Show($"Вы уверены? Удалится {ClientForRemoving.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    abd1Entities.GetContext().PRClient.RemoveRange(ClientForRemoving);
                    abd1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены! ");
                    DGridProject.ItemsSource = abd1Entities.GetContext().PRClient.ToList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditClient((sender as Button).DataContext as PRClient));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProject.ItemsSource = abd1Entities.GetContext().PRClient.ToList();
            }
        }

        private void DGridProject_Loaded(object sender, RoutedEventArgs e)
        {
            //if (type == 1)
            //{
            //    rad.Visibility = Visibility.Visible;
            //    ToAuto.Visibility = Visibility.Visible;
            //    ToProcat.Visibility = Visibility.Visible;
            //}
            //else if (type == 2)
            //{
            //    ToClient.Visibility = Visibility.Collapsed;
            //    ToAuto.Visibility = Visibility.Collapsed;
            //    ToProcat.Visibility = Visibility.Visible;
            //}
            //else if (type == 3)
            //{
            //    ToClient.Visibility = Visibility.Collapsed;
            //    ToAuto.Visibility = Visibility.Visible;
            //    ToProcat.Visibility = Visibility.Collapsed;
            //}
        }
    }
}
