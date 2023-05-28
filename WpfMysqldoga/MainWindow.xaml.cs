using System;
using System.Collections;
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
using MySql.Data.MySqlClient;

namespace EuroGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=eurovizio;";
        List<Eurovizio> euro = new List<Eurovizio>();
        MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            AdatbazisMegnyitas();
            Datagridbeiras();


            AdatbazisBezarasa();
        }

      

        private void AdatbazisMegnyitas()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception)
            {

                MessageBox.Show("Nem tud kapcsolódni az adatbázishoz");
                this.Close();
            }
        }

        private void Datagridbeiras()
        {
            string osszes = "SELECT ev,eloado,cim,helyezes,pontszam FROM dal";
            MySqlCommand command = new MySqlCommand(osszes, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Eurovizio uj = new Eurovizio(reader.GetInt32("ev"),
                                        reader.GetString("eloado"),
                                            reader.GetString("cim"),
                                            reader.GetInt32("helyezes"),
                                            reader.GetInt32("pontszam"));
                euro.Add(uj);
            }
            reader.Close();
            dataGrid.ItemsSource = euro;

        }

        private void AdatbazisBezarasa()
        {
            connection.Close();
            connection.Dispose();
        }

        private void btnFeladat4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdatbazisMegnyitas();
                {
                    var command = new MySqlCommand("SELECT COUNT(*) FROM dal WHERE orszag = 'Magyarország'", connection);
                    var eredmeny = command.ExecuteScalar();
                    var MOversenyzok = Convert.ToInt32(eredmeny);

                    command = new MySqlCommand("SELECT MAX(helyezes) FROM dal WHERE orszag = 'Magyarország'", connection);
                    eredmeny = command.ExecuteScalar();
                    var legjobbhelyezes = eredmeny == DBNull.Value ? 0 : Convert.ToInt32(eredmeny);

                    MessageBox.Show($"Magyarországi versenyzők száma: {MOversenyzok}\nLegjobb helyezés: {legjobbhelyezes}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt! \n" + ex.Message);
            }
        }


        private void btnFeladat5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdatbazisMegnyitas();
                {
                    var command = new MySqlCommand("SELECT ROUND(AVG(pontszam), 2) FROM dal WHERE orszag = 'Németország'", connection);
                    var eredmeny = command.ExecuteScalar();
                    var atlag = Convert.ToDouble(eredmeny);

                    MessageBox.Show($"Németország átlagos pontszáma: {atlag:F2}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt! \n" + ex.Message);
            }
        }

        private void btnFeladat6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdatbazisMegnyitas();
                {
                    var parancs = new MySqlCommand("SELECT CONCAT(eloado, ' - ', cim) FROM dal WHERE cim LIKE '%Luck%'", connection);
                    var reader = parancs.ExecuteReader();

                    var szerepelaluckszo = string.Empty;
                    while (reader.Read())
                    {
                        szerepelaluckszo += reader.GetString(0) + ", ";
                    }

                    reader.Close();

                    if (!string.IsNullOrEmpty(szerepelaluckszo))
                    {
                        szerepelaluckszo = szerepelaluckszo.TrimEnd(',', ' ');
                        MessageBox.Show(szerepelaluckszo);
                    }
                    else
                    {
                        MessageBox.Show("Nincsenek olyan dalok, amelyekben szerepel a \"Luck\" szó.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt! \n" + ex.Message);
            }
        }

        private void btnFeladat7_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnFeladat8_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
