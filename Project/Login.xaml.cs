using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                List<user> userLoginPass = abd1Entities.GetContext().user.ToList();

            
            foreach (user checkUser in userLoginPass)
            {
                if(checkUser.Login == loginBox.Text && checkUser.Password == passBox.Text)
                {
                    NavigationService.Navigate(new HelloPage((int)checkUser.TypeRole)) ;
                    return;
                }

            }
            MessageBox.Show("Неверно " + loginBox.Text + " /");


        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            loginBox.Items.Clear();


            abd1Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            List<user> userLoginPass = abd1Entities.GetContext().user.ToList();
            foreach (user checkUser in userLoginPass)
            {
                loginBox.Items.Add(checkUser.Login);

            }
        }
    }
}
