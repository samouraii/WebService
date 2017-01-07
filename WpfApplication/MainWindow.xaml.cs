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
using WpfApplication.wcfClient;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WcfUser.WcfUserClient client;
        private wcfClient.WcfClientClient bddClient;
        private User temporaireUser;
        public MainWindow()
        {
            InitializeComponent();
            client = new WcfUserClient();
            bddClient = new WcfClientClient();
        }
        private void zero()
        {
            clearError();
            temporaireUser = null;
            UserC.Visibility = Visibility.Hidden;
            UserM.Visibility = Visibility.Hidden;
            suppression.Visibility = Visibility.Hidden;
            GridClient.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.zero();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            zero();
            UserC.Visibility = Visibility.Visible;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            User create = new User();



            create.username = this.username.Text;
           WcfUser.Error err = new WcfUser.Error();

            if (pass1.Password == pass2.Password && pass1.Password != null && temporaireUser == null)
            {

                create.mdp = pass1.Password;

                err = client.insert(create);

            }
            else if (temporaireUser != null)
            {
                if ((pass1.Password != null || pass1.Password != "") && pass1.Password == pass2.Password)
                {
                    temporaireUser.mdp = pass1.Password;
                }
                temporaireUser.username = username.Text;
                err = client.update(temporaireUser);
            }
            else
            {
                Errerur("Les 2 mots de passe doivent être les mêmes");
            }
            if (err.code == 1)
            {
                zero();
                username.Text = "";
                pass1.Password = "";
                pass2.Password = "";
                clearError();
                Errerur("utilisateur créer");
            }
            else
            {
                Errerur(err.code + " " + err.message);
            }
        }

        private void Errerur(string e)
        {
            Error.Content += e + "\n";
        }
        private void clearError()
        {
            Error.Content = "";
        }
        /*diparaitre*/
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            User[] t = client.select();
           
            foreach (User i in t)
            {
                Console.WriteLine(i.username + " " + i.id);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            User[] t = client.select();
            dataUser.ItemsSource = t;

            UserM.Visibility = Visibility.Visible;

        }

        private void dataUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            zero();

            var t = ((DataGrid)sender).CurrentCell.Item;

            temporaireUser = (User)t;

            UserC.Visibility = Visibility.Visible;
            suppression.Visibility = Visibility.Visible;
            username.Text = temporaireUser.username;
            pass1.Password = "";
            pass2.Password = "";

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Voulez vous suprimez l'utilisateur?", "Suppression", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                client.delete(temporaireUser);
            }
            zero();
            UserM.Visibility = Visibility.Visible;
        }

        private void ClientCree(object sender, RoutedEventArgs e)
        {
            Client wcfClients = new Client();
           // wcfClients.enable = true;
            wcfClients.nom = clientName.Text;
            int temp = -1;
            int.TryParse(clientID.Text,out temp );            
            wcfClients.numdossier = temp;
            wcfClients.numTva = clientTva.Text;
            wcfClient.Error erreur = new wcfClient.Error();
            erreur.code = 4;
            erreur.message = "succes création";
            if (temp >= 0)
            {
               erreur = bddClient.insert(wcfClients);
            }
            Errerur(erreur.message);
        }
    
        private void creeClientForm(object sender, RoutedEventArgs e)
        {
            zero();
            GridClient.Visibility = Visibility.Visible;
            
        }
    }
}
