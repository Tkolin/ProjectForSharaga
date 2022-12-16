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
    /// Логика взаимодействия для AddEditAuto.xaml
    /// </summary>
    public partial class AddEditAuto : Page
    {
        private PRAuto _CurrentAuto = new PRAuto();
        bool write = true;
        public AddEditAuto(PRAuto Selected_Auto,bool write)
        {
            this.write = write;
            InitializeComponent();
            if (Selected_Auto != null)
            {
                _CurrentAuto = Selected_Auto;
            }

            DataContext = _CurrentAuto;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_CurrentAuto.Model))

                errors.AppendLine("Заполните все поля");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_CurrentAuto.ID == 0)
                abd1Entities.GetContext().PRAuto.Add(_CurrentAuto);

            try
            {
                abd1Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
        
                t1.IsEnabled = write;
            t2.IsEnabled = write;
            t3.IsEnabled = write;
            t4.IsEnabled = write;
            t5.IsEnabled = write;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
