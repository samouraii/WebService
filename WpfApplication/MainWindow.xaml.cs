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
using System.Reflection;
using WpfApplication.Auth;
namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WcfUser.WcfUserClient client;
        private wcfClient.WcfClientClient bddClient;
        private Auth.AuthentificationClient AuthClient;
        private WcfUser.User temporaireUser;
        private wcfClient.Client temporaireClient;
        private Jeton jeton;
        public MainWindow()
        {
            

            InitializeComponent();
            client = new WcfUserClient();
            bddClient = new WcfClientClient();
            AuthClient = new AuthentificationClient();

            menu.IsEnabled = false;
            GridLogin.Visibility = Visibility.Visible;


        }
        private void zero()
        {
            clearError();
            temporaireUser = null;
            temporaireClient = null;
            UserC.Visibility = Visibility.Hidden;
            UserM.Visibility = Visibility.Hidden;
            suppression.Visibility = Visibility.Hidden;
            GridClient.Visibility = Visibility.Hidden;
            ClientM.Visibility = Visibility.Hidden;
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
            WcfUser.User create = new WcfUser.User();



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
            WcfUser.User[] t = client.select(null);

            foreach (WcfUser.User i in t)
            {
                Console.WriteLine(i.username + " " + i.id);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            zero();
            WcfUser.User[] t = client.select(null);
            dataUser.ItemsSource = t;

            UserM.Visibility = Visibility.Visible;

        }

        private void dataUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            zero();

            var t = ((DataGrid)sender).CurrentCell.Item;

            temporaireUser = (WcfUser.User)t;

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
            wcfClient.Client wcfClients = new wcfClient.Client();
            if (temporaireClient != null)  wcfClients = temporaireClient;
             
            // wcfClients.enable = true;
            wcfClients.nom = clientName.Text;
            int temp = -1;

      
            
            int.TryParse(clientID.Text, out temp);
            wcfClients.numdossier = temp;
            wcfClients.numtva = clientTva.Text;
            wcfClient.User[] u = new wcfClient.User[1];
            u[0] = new wcfClient.User();
            u[0].username = jeton.user.username;
            u[0].id = jeton.user.id;
            wcfClients.user = u;
            wcfClient.Error erreur = new wcfClient.Error();
            erreur.code = 4;
            erreur.message = "succes création";
            if (temp >= 0)
            {
                erreur = bddClient.insert(wcfClients);
            }
            this.zero();
            Errerur(erreur.message);
            
        }

        private void creeClientForm(object sender, RoutedEventArgs e)
        {
            zero();
            GridClient.Visibility = Visibility.Visible;

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            test t = new test();
            Type type = t.GetType();
            PropertyInfo method = type.GetProperty("Toto");

            object[] obj =(object[]) method.GetValue(t);

            Type type2 = obj[0].GetType();
            //faut vérifier que le type ne soit pas null, avant de le faire. si pas d'object alors on oublie la méthode. 


           PropertyInfo method2 = type2.GetProperty(type.Name[0].ToString().ToUpper() + type.Name.Remove(0,1));
            //MessageBox.Show(getBDD("tutu","toto"));
            MessageBox.Show(method2.PropertyType.IsArray+ "");
            MessageBox.Show(("tutu".GetType() ==  typeof(string)).ToString() );
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            this.clearError();
            Auth.User user = new Auth.User();
            user.username = login.Text;
            user.mdp = passwordBox.Password;
           Jeton j = AuthClient.login(user);
            if (j.erreur == null)
            {
                jeton = j;
                GridLogin.Visibility = Visibility.Hidden;
                menu.IsEnabled = true;

            }
            else
            {
                Errerur(j.erreur.message);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            zero();
            wcfClient.Client[] t = bddClient.select();
            dataClient.ItemsSource = t;

            ClientM.Visibility = Visibility.Visible;
        }

        private void dataClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            zero();

            var t = ((DataGrid)sender).CurrentCell.Item;

            temporaireClient = (wcfClient.Client)t;
            this.DataContext = temporaireClient;
            

            GridClient.Visibility = Visibility.Visible;
           // CreeClient.Visibility = Visibility.Visible;
          
        }

        private void ClientSupp(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Voulez vous suprimez l'utilisateur?", "Suppression", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                this.bddClient.delete(temporaireClient);
            }
            zero();
            UserM.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            achat f = new achat();
            f.ShowDialog();

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            achat f = new achat(1);
            f.ShowDialog();
        }
    }
  
}
