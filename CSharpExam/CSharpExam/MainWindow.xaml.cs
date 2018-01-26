using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace CSharpExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Yhistu> _yhistud;
       

        public MainWindow()
        {
            InitializeComponent();
            LaeAndmedFailist();
            _yhistud = new List<Yhistu>();
            
        }

        private void btn_lisayhistu_Click(object sender, RoutedEventArgs e)
        {

            if (txb_aadress.Text == "" || txb_registrikood.Text == "" || txb_yhistunimi.Text == "")
            {
                MessageBox.Show("Palun täitke kõik väljad!");
            }

            else
            {
                if (lsb_yhistud.SelectedIndex < 0)
                {
                    bool kas_yhistu_eksisteerib = _yhistud.Any(x => x._registrikood == long.Parse(txb_registrikood.Text));

                    if (kas_yhistu_eksisteerib)
                    {
                        MessageBox.Show("Selline ühistu on juba lisatud!");
                    }

                    else

                    {
                        //uue korteriühistu loomine
                        Yhistu uusyhistu = new Yhistu();
                        uusyhistu._yhistunimi = txb_yhistunimi.Text;
                        uusyhistu._registrikood = long.Parse(txb_registrikood.Text);
                        uusyhistu._yhistuaadress = txb_aadress.Text;
                        uusyhistu._yhistuloomiseaeg = DateTime.Now;

                        uusyhistu._korterid = new List<Korter>();

                       
                        

                        _yhistud.Add(uusyhistu);
                        lsb_yhistud.ItemsSource = _yhistud;

                    }
                }

                else

                {
                    //ühistu andmete muutmine
                    var muudetavyhistu = lsb_yhistud.SelectedItem as Yhistu;
                    muudetavyhistu._yhistuaadress = txb_aadress.Text;
                    muudetavyhistu._registrikood = long.Parse(txb_registrikood.Text);
                    muudetavyhistu._yhistunimi = txb_yhistunimi.Text;
                    muudetavyhistu._yhistuloomiseaeg = DateTime.Now;
                    btn_lisayhistu.Content = "Lisa ühistu";
                    lsb_yhistud.SelectedIndex = -1;
                }
                lsb_yhistud.Items.Refresh();

            }
        }

        private void btn_eemaldayhistu_Click(object sender, RoutedEventArgs e)
        {
            if(lsb_yhistud.SelectedIndex>-1)
            {
                var eemaldatavyhistu = lsb_yhistud.SelectedItem as Yhistu;

                if(eemaldatavyhistu!=null)
                {
                    _yhistud.Remove(eemaldatavyhistu);
                }
               

            }
            else

            {
                MessageBox.Show("Valige esmalt ühistu, mida eemaldada!");
            }
            lsb_yhistud.Items.Refresh();
            btn_lisayhistu.Content = "Lisa ühistu";
            TyhjendaValjad();
        }

        private void lsb_yhistud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //kui listboksis valitakse mõni ühistu, siis kuvatakse tekstiväljadele vastavad andmed

            if (lsb_yhistud.SelectedIndex > -1)
            {
                var valitudyhistu = lsb_yhistud.SelectedItem as Yhistu;
                txb_yhistunimi.Text = valitudyhistu._yhistunimi;
                txb_registrikood.Text = valitudyhistu._registrikood.ToString();
                txb_aadress.Text = valitudyhistu._yhistuaadress;

                btn_lisayhistu.Content = "Muuda ühistut";

                //kuvame ühistu juurde käivad korterid
                lsb_korterid.ItemsSource = valitudyhistu._korterid;


            }
            lsb_yhistud.Items.Refresh();

        }

        private void btn_lisakorter_Click(object sender, RoutedEventArgs e)
        {
            if(lsb_yhistud.SelectedIndex>-1)
            {

                if (txb_vastutavisik.Text == "" || txb_korteritunnus.Text == "" || txb_tubadearv.Text == "" || txb_ruutmeetrid.Text == "")
                {
                    MessageBox.Show("Vaja on täita kõik väljad!");

                }

                else

                {
                    var valitudyhistu = lsb_yhistud.SelectedItem as Yhistu;
                    if (valitudyhistu != null)
                    {
                        Korter korter = new Korter(txb_vastutavisik.Text, txb_korteritunnus.Text, int.Parse(txb_tubadearv.Text), int.Parse(txb_ruutmeetrid.Text), DateTime.Now);
                        valitudyhistu.LisaKorter(korter);
                        MessageBox.Show("Korter lisatud");
                        lsb_korterid.Items.Refresh();
                        TyhjendaValjad();
                        
                        btn_lisayhistu.Content = "Lisa ühistu";
                        lsb_yhistud.SelectedIndex = -1;
                        TyhjendaValjad();
                    }
                }
                
            }
            else

            {
                MessageBox.Show("Valige esmalt ühistu, millele kortereid lisada!");
            }
        }

        private void rdb_nimi_Checked(object sender, RoutedEventArgs e)
        {
            txb_otsi.IsEnabled = true;
        }

        private void rdb_aadress_Checked(object sender, RoutedEventArgs e)
        {
            txb_otsi.IsEnabled = true;
        }

        private void btn_otsi_Click(object sender, RoutedEventArgs e)
        {
            //otsime nime järgi
            if(rdb_nimi.IsChecked==true && txb_otsi.Text!="")
            {
                lsb_tulemused.ItemsSource = _yhistud.Where(x => x._yhistunimi.Equals(txb_otsi.Text)).OrderBy(x=>x._yhistunimi);
                if(lsb_tulemused.Items.Count == 0)
                {
                    MessageBox.Show("Tulemusi ei leitud");
                }
            }
            //otsime aadressi järgi
            else if(rdb_aadress.IsChecked == true && txb_otsi.Text != "")
            {
                lsb_tulemused.ItemsSource = _yhistud.Where(x => x._yhistuaadress.Equals(txb_otsi.Text));
                if (lsb_tulemused.Items.Count == 0)
                {
                    MessageBox.Show("Tulemusi ei leitud");
                }
            }
            else

            {
                MessageBox.Show("Palun sisestage otsingusõna!");
            }
            txb_otsi.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (var sw = new StreamWriter("Yhistu.xml"))
            {
                var xs = new XmlSerializer(typeof(List<Yhistu>));
                xs.Serialize(sw, _yhistud);
            }
        }

        public void LaeAndmedFailist()
        {
            if (File.Exists("Yhistu.xml"))
            {
                using (var sw = new StreamReader("Yhistu.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Yhistu>));
                    List<Yhistu> yhistud = (List<Yhistu>)xs.Deserialize(sw);
                    lsb_yhistud.ItemsSource = yhistud;
                }
                    
            }
        }

        public void TyhjendaValjad()
        {
            txb_ruutmeetrid.Clear();
            txb_tubadearv.Clear();
            txb_vastutavisik.Clear();
            txb_korteritunnus.Clear();

            txb_yhistunimi.Clear();
            txb_registrikood.Clear();
            txb_aadress.Clear();
        }



        

    }
}
