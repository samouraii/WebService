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
using System.Windows.Shapes;
using WpfApplication.wcfClient;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for achat.xaml
    /// </summary>
    public partial class achat : Window
    {
        private wcfClient.WcfClientClient bddClient;
        private wcfClient.Client client;
        private wcfClient.Transaction temporaireTransaction;
        private int mode;
        public achat(int mode =0)
        {
            InitializeComponent();
            this.bddClient = new WcfClientClient(); ;
            this.mode = mode;
        }

        /*=============================================
                Chargement de la fenetre tout est remis à 0
            =======================================
            */

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wcfClient.Client[] t = bddClient.select();
            choice.ItemsSource = t;
            gridChoice.Visibility = Visibility.Visible;
            gridInsert.Visibility = Visibility.Hidden;
            button1.Visibility = Visibility.Hidden;
            gridhistorique.Visibility = Visibility.Hidden;



        }

        /*=============================================
          Choix du client et mise en place de l'afficahge suivant
        =======================================
        */
        private void dataUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridChoice.Visibility = Visibility.Hidden;
            var t = ((DataGrid)sender).CurrentCell.Item;

            client = (Client)t;
            wcfClient.Transaction tr = new wcfClient.Transaction();
            if (temporaireTransaction != null) tr = temporaireTransaction;
            temporaireTransaction = tr;
            
            this.DataContext = tr;

            if (mode == 0) gridInsert.Visibility = Visibility.Visible;
            else {
                gridhistorique.Visibility = Visibility.Visible;
               Transaction[] tra = bddClient.selectT(client);
                histo.ItemsSource = tra;
            }

        }
        /*=============================================
            Gestion du prix tvac
        =======================================
        */
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double x, y = 0;
                double.TryParse(textBox.Text, out x);
                double.TryParse(textBox1.Text, out y);
                prix.Content = y * (1 + x / 100);

            }
            catch (Exception ex)
            {
                

            }
        }
        /*=============================================
           Gestion de la sauvegarde
        =======================================
        */

        private void button_Click(object sender, RoutedEventArgs e)
        {



            temporaireTransaction.client = client;
            //tr.client = client;
            
            if (temporaireTransaction.prixhtv !=0 && temporaireTransaction.tva != null && temporaireTransaction.voiture != null && temporaireTransaction.voiture.Length >= 3)
            {
                Error err = bddClient.insertT(temporaireTransaction);

                // MessageBox.Show(temporaireTransaction.tva + "");
                // wcfClients.enable = true;
                if (mode == 0)
                {
                    temporaireTransaction = null;
                    //tr.client = client;
                    this.DataContext = temporaireTransaction;
                    if (err.code <= 100)
                    MessageBox.Show("insertion avec succes");
                    else MessageBox.Show(err.message);
                }
                else
                {
                    Transaction[] tra = bddClient.selectT(client);
                    histo.ItemsSource = tra;
                    gridInsert.Visibility = Visibility.Hidden;
                    gridhistorique.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("faut tout remplire correctement");
            }
        }
        /*=============================================
           Gestion du double clique sur l'historique pour modifier, et afficher l'historique 
        =======================================
        */
        private void datahisto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            gridhistorique.Visibility = Visibility.Hidden;
            var t = ((DataGrid)sender).CurrentCell.Item;

            temporaireTransaction = (Transaction)t;
           
            

            this.DataContext = temporaireTransaction;

            gridInsert.Visibility = Visibility.Visible;
            button1.Visibility = Visibility.Visible;



        }    /*=============================================
                Gestion du bouton de suppression
            =======================================
            */

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Voulez vous suprimez la transaction?", "Suppression", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                this.bddClient.deleteT(temporaireTransaction);
            }
            Transaction[] tra = bddClient.selectT(client);
            histo.ItemsSource = tra;
            gridInsert.Visibility = Visibility.Hidden;
            gridhistorique.Visibility = Visibility.Visible;
        }

       
    }
}
