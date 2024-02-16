using DataAcces.Repositories;
using Model.Models;
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

namespace Whatsapp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page 
    {

        public List<User> Users { get; set; }
        public User LoginedUser { get; set; }

        public LoginPage()
            
        {
            Users = new BaseRepo<User>().GetAll().ToList(); ;
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var user = Users.Where(u => (u.UserName == username.Text) &&( u.Password == password.Text));
            if (user.Count()>0)
            {
                LoginedUser = user.First();
                MessageBox.Show("Alindi");
                this.NavigationService.Navigate(new MessagePage(LoginedUser));
            }   
          
        }
    }
}
