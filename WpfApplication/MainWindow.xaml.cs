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
using WpfApplication.WcfUser;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



            UserC.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            UserC.Visibility = Visibility.Visible;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            User create = new User();
            create.username = this.username.Text;
            Error err = new Error();
            if (pass1.Password == pass2.Password)
            {

                create.mdp = pass1.Password;
                WcfUser.WcfUserClient client = new WcfUserClient();
               err= client.insert(create);
                if (err.code == 1) {
                    UserC.Visibility = Visibility.Hidden;
                    username.Text = "";
                    pass1.Password = "";
                    pass2.Password = "";
                    clearError();
                    Errerur("utilisateur créer");
                }
                else
                {
                    Errerur(err.code + " "+ err.message);
                }
            }
            else
            {
                Errerur("Les 2 mots de passe doivent être les mêmes");
            }
        }

        private void Errerur(string e) {
            Error.Content += e+"\n";
        }
        private void clearError()
        {
            Error.Content = "";
        }
    }
}
