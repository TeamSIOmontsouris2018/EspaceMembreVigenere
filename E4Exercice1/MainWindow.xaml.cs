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
using MySql.Data.MySqlClient;
using System.Data;

namespace E4Exercice1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class Bdd
        {

            private MySqlConnection connexion;

            // Constructeur
            public Bdd()
            {
                this.InitConnexion();
            }

            // Méthode pour initialiser la connexion
            private void InitConnexion()
            {
                // Création de la chaîne de connexion
                string connectionString = "SERVER=127.0.0.1; DATABASE=e4exercice1; UID=root; PASSWORD=";
                this.connexion = new MySqlConnection(connectionString);
            }

            // Méthode pour ajouter une personne
            public void AddPersonne(string nom, string prenom, string societe, string password)
            {
                Cryptage crpt = new Cryptage();
                string phrase = "";
                try
                {
                    // Ouverture de la connexion SQL
                    this.connexion.Open();

                    // Création d'une commande SQL en fonction de l'objet connection
                    MySqlCommand cmd = this.connexion.CreateCommand();

                    // Requête SQL
                    cmd.CommandText = "INSERT INTO personnes (ID, Prenom, Nom, Societe, Password) VALUES (@ID, @Prenom, @Nom, @Societe, @Password)";

                    // utilisation de l'objet personne passé en paramètre
                    cmd.Parameters.AddWithValue("@ID", "1");
                    cmd.Parameters.AddWithValue("@Prenom", prenom);
                    cmd.Parameters.AddWithValue("@Nom", nom);
                    cmd.Parameters.AddWithValue("@Societe", societe);
                    cmd.Parameters.AddWithValue("@Password", crpt.Vigenere(password));

                    // Exécution de la commande SQL
                    cmd.ExecuteNonQuery();

                    // Fermeture de la connexion
                    this.connexion.Close();
                }
                catch
                {
                    // Gestion des erreurs :
                    // Possibilité de créer un Logger pour les exceptions SQL reçus
                    // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
                }
            }

            public bool AccountChecking(string nom, string prenom, string societe)
            {
                MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1; DATABASE=e4exercice1; UID=root; PASSWORD=");
                int i = 0;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM personnes WHERE nom = '" + nom + "' AND prenom = '" + prenom + "' AND societe = '" + societe + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                i = Convert.ToInt32(dt.Rows.Count.ToString());

                if (i >= 1)
                {
                    MessageBox.Show("Ce compte existe déjà !");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public bool SeConnecter(string id, string password)
            {
                Cryptage crpt = new Cryptage();

                MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1; DATABASE=e4exercice1; UID=root; PASSWORD=");
                int i = 0;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM personnes WHERE id = '" + id + "' AND password = '" + crpt.Vigenere(password) + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public class Cryptage
        {
            int indice = 0; int Plettre = 0; int Dlettre = 0;
            string colettre = ""; string test = ""; string phrase = "";
            int indiph = 0; int i = 0; int z = 0; int coz = 0;
            string Recupercogi; string alpha = "abcdefghijklmnopqrstuvwxyz";
            
            string cogito = "cogito";

            
            //phrase = "phrase";
            public string Vigenere(string phrase)
            {
                while (indiph < phrase.Length)
                {
                    Recupercogi = cogito[coz].ToString();

                    while (alpha[z].ToString() != Recupercogi)
                    {
                        z = z + 1;
                    }
                    test = phrase[indiph].ToString();

                    if (test == " ")
                    {
                        indice = indiph + 1;
                        colettre = colettre + " ";
                    }
                    else
                    {
                        while (alpha[i].ToString() != test)
                        {
                            i = i + 1;
                        }
                        i = i + z;
                        if (i >= 26)
                        {
                            i = i - 26;
                        }
                        colettre = colettre + alpha[i].ToString();
                        z = 0;
                        i = 0;
                        indiph = indiph + 1;

                        coz = coz + 1;
                        if (coz >= 6)
                        {
                            coz = 0;
                        }
                    }
                }
                return (colettre);
            }
            
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Vérifications
            if (String.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Le champ 'nom' n'a pas été rempli !");
            }
            else if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Le champ 'prénom' n'a pas été rempli !");
            }
            else if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Le champ 'société' n'a pas été rempli !");
            }
            else if (String.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Le champ 'mot de passe' n'a pas été rempli !");
            }
            else
            {
                Bdd a = new Bdd();
                MainWindow b = new MainWindow();
                if (a.AccountChecking(textBox.Text, textBox1.Text, textBox2.Text) == false)
                {
                    a.AddPersonne(textBox.Text, textBox1.Text, textBox2.Text, passwordBox.Password);
                    MessageBox.Show("Votre compte a bien été créé, " + textBox1.Text + ".");
                }
            }
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Vérifications
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Le champ 'ID' n'a pas été rempli !");
            }
            else if (String.IsNullOrEmpty(passwordBox1.Password))
            {
                MessageBox.Show("Le champ 'mot de passe' n'a pas été rempli !");
            }
            else
            {
                Bdd a = new Bdd();
                MainWindow b = new MainWindow();
                if (a.SeConnecter(textBox3.Text, passwordBox1.Password) == true)
                {
                    MessageBox.Show("Vous êtes connecté, " + textBox3.Text + ".");
                }
                else
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect. Réessayez.", "Eh non...");
                }
            }
        }

    }
}
