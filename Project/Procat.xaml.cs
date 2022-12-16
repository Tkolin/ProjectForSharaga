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
    /// Логика взаимодействия для Procat.xaml
    /// </summary>
    public partial class Procat : Page
    {
        int type;
        List<string> modelName = new List<string>();
        List<PRProcat> _procatS = new List<PRProcat>();
        public Procat(int type)
        {
            this.type = type;
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProject.ItemsSource = abd1Entities.GetContext().PRProcat.ToList();

                foreach(PRProcat _procat in abd1Entities.GetContext().PRProcat.ToList())
                {
                    foreach(PRAuto _auto in abd1Entities.GetContext().PRAuto.ToList())
                    {
                        if(_procat.AutoID == _auto.ID)
                        {
                            
                            modelName.Add(_auto.Model);
                        }
                    }
                }
            }

            ModelComboBox.ItemsSource = modelName.Distinct();

            _procatS = abd1Entities.GetContext().PRProcat.ToList();

        }

        private void BtnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProject.ItemsSource = abd1Entities.GetContext().PRProcat.ToList();
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            List<PRProcat> vievTable = new List<PRProcat>();
            if (Visibility == Visibility.Visible)
            {
               
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                foreach (PRProcat i in abd1Entities.GetContext().PRProcat.ToList())
                { 
                    DateTime endTime = ((DateTime)i.StartTime).AddDays((int)i.DayCount);
                    if (endTime < DateTime.Now)
                    {                   
                        vievTable.Add(i);
                    }
                }
            }
            DGridProject.ItemsSource = vievTable;
        }
        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            List<PRProcat> vievTable = new List<PRProcat>();

            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                foreach (PRProcat i in abd1Entities.GetContext().PRProcat.ToList())
                {
                    DateTime endTime = ((DateTime)i.StartTime).AddDays((int)i.DayCount);
                    if (endTime >= DateTime.Now)
                    {
                        vievTable.Add(i);
                    }
                }
            }
            DGridProject.ItemsSource = vievTable;
        }

        private void DGridProject_Loaded(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case 1:
                    bu1.Visibility = Visibility.Collapsed;
                    bu2.Visibility = Visibility.Collapsed;
                    bu3.Visibility = Visibility.Collapsed;
                    ModelComboBox.Visibility = Visibility.Visible;
                    break;
                case 2:
                    bu1.Visibility = Visibility.Visible;
                    bu2.Visibility = Visibility.Visible;
                    bu3.Visibility = Visibility.Visible;
                    ModelComboBox.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    bu1.Visibility = Visibility.Collapsed;
                    bu2.Visibility = Visibility.Collapsed;
                    bu3.Visibility = Visibility.Collapsed;
                    ModelComboBox.Visibility = Visibility.Collapsed;

                    break;


            }
        }

        private void Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridProject.ItemsSource = abd1Entities.GetContext().PRProcat.ToList();
            if (Visibility == Visibility.Visible)
            {
                abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                _procatS.Clear();
                DGridProject.ItemsSource = _procatS;

                if (ModelComboBox.SelectedItem.ToString() == "")
                {
                    return;
                }
                foreach (PRProcat _procat in abd1Entities.GetContext().PRProcat.ToList())
                {
                    foreach (PRAuto _auto in abd1Entities.GetContext().PRAuto.ToList())
                    {
                        if (_procat.AutoID == _auto.ID)
                        {

                            if (_auto.Model == ModelComboBox.SelectedItem.ToString())
                            {
                                _procatS.Add(_procat);
                                
                            }
                        }
                    }
                }




            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProcat(null));
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridProject.ItemsSource = abd1Entities.GetContext().PRProcat.ToList();
        }
    }
}
