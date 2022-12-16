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
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Page
    {
        int type;
        bool writeAuto = false;
        public Auto(int type)
        {
            this.type = type;
            if(type == 1)
                writeAuto = true;
            else
                writeAuto = false;
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAuto((sender as Button).DataContext as PRAuto, writeAuto));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAuto(null, writeAuto));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var ClientForRemoving = DGridProject.SelectedItems.Cast<PRAuto>().ToList();

            if (MessageBox.Show($"Вы уверены? Удалится {ClientForRemoving.Count()} элемент(ов)?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    abd1Entities.GetContext().PRAuto.RemoveRange(ClientForRemoving);
                    abd1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены! ");
                    DGridProject.ItemsSource = abd1Entities.GetContext().PRAuto.ToList();

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
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProject.ItemsSource = abd1Entities.GetContext().PRAuto.ToList();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filterBox.Text == "" && filterBoxGos.Text == "")
            {
                DGridProject.ItemsSource = abd1Entities.GetContext().PRAuto.ToList();
                return;
            }
            List<PRAuto> vievTable = new List<PRAuto>();
            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                foreach (PRAuto i in abd1Entities.GetContext().PRAuto.ToList())
                {
                    if(filterBox.Text.Length > 0 && filterBoxGos.Text.Length > 0)
                    {
                        if (i.Model.ToLower().IndexOf(filterBox.Text.ToLower()) != -1 & i.StNumber.ToLower().IndexOf(filterBoxGos.Text.ToLower()) != -1)
                            vievTable.Add(i);
                      

                    }
                    else if (filterBox.Text.Length > 0)
                    {
                        if (i.Model.ToLower().IndexOf(filterBox.Text.ToLower()) != -1)
                            vievTable.Add(i);
                       
                    }
                    else if (filterBoxGos.Text.Length > 0)
                    {
                        if (i.StNumber.ToLower().IndexOf(filterBoxGos.Text.ToLower()) != -1)
                                vievTable.Add(i);
                      
                    }
                    else
                    {
                        
                    }
                }
                DGridProject.ItemsSource = vievTable;
            }

        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case 1:
                    gosNumbGrid.Visibility = Visibility.Visible;
                    modelGrid.Visibility = Visibility.Visible;
                    break;
                case 2:
                    gosNumbGrid.Visibility = Visibility.Visible;
                    modelGrid.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    BtnAdd.Visibility = Visibility.Collapsed;
                    BtnDel.Visibility = Visibility.Collapsed;
                    break;


            }
        }

        private void filterBoxGos_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DGridProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        private void DGridProject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            NavigationService.Navigate(new AddEditAuto((PRAuto)DGridProject.SelectedItem, writeAuto));
        }
    }
}
