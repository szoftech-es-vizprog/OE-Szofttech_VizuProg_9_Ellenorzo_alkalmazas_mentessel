using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Ellenorzo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tantargy> tantargyak = new List<Tantargy>();
        Tantargy valasztottTargy;
        int modositandoIndex;
        public MainWindow()
        {
            InitializeComponent(); 

        }
        private void targy_hozzadas_Click(object sender, RoutedEventArgs e)
        {
            string targynev_atmeneti = targynev.Text.ToLower();

            List<Tantargy> azonos = tantargyak.FindAll(i => i.NevKicsi == targynev_atmeneti);
            if( azonos.Count == 0)
            {
                tantargyak.Add(new Tantargy(targynev.Text));
            }
            /* Adatkötéssel frissítem a combo boxot*/
            /*
             * targyvalaszto.ItemsSource = tantargyak;
             * targyvalaszto.DisplayMemberPath = "Nev";
             */
            /* Egyzserűen hozzáadom a Combobox elmeihez */
            targyvalaszto.Items.Add(targynev.Text);

            targynev.Text = "Tárgy neve";
        }
        private void targyValasztas_Click(object sender, RoutedEventArgs e)
        {
            modositandoIndex = -1;
            jegyModositasa.Text = "Modosítandó jegy"; 
            jegyek.Items.Clear();
            if ( targyvalaszto.SelectedIndex > -1)
            {
                valasztottTargy = tantargyak.ElementAt(targyvalaszto.SelectedIndex);

                foreach (var item in valasztottTargy.Jegyek)
                {
                    jegyek.Items.Add(item.Erdemjegy) ;
                }
            }
            atlagMegjelenes.Content = valasztottTargy.Atlag().ToString("0.00");
        }
        private void jegyTorles_Click(object sender, RoutedEventArgs e)
        {
            valasztottTargy.Jegyek.RemoveAt( jegyek.SelectedIndex);
            JegyekFrissitese();
        }
        private void JegyekFrissitese()
        {

            jegyek.Items.Clear();
                foreach (var item in valasztottTargy.Jegyek)
                {
                    jegyek.Items.Add(item.Erdemjegy);
                }
            atlagMegjelenes.Content = valasztottTargy.Atlag().ToString("0.00");
        }
        private void ujJegyMentese_Click(object sender, RoutedEventArgs e)
        {
            int jegy = Int32.Parse( ujJegy.Text);
            if( jegy >0 && jegy < 6)
            {
                valasztottTargy.Jegyek.Add(new Jegy(jegy));
            }
            JegyekFrissitese();
            ujJegy.Text = "Új jegy rögzítése";
        }
        private void modositas_mentes_Click(object sender, RoutedEventArgs e)
        {
            if (modositandoIndex > -1)
            {
                int jegy = Int32.Parse(jegyModositasa.Text);
                if (jegy > 0 && jegy < 6)
                {
                    valasztottTargy.Jegyek[modositandoIndex].Erdemjegy = jegy;
                }
                JegyekFrissitese();
                modositandoIndex = -1;
                jegyModositasa.Text = "Modosítandó jegy";
            }
        }
        private void modositashoz_Click(object sender, RoutedEventArgs e)
        {
            modositandoIndex = jegyek.SelectedIndex;
            jegyModositasa.Text = jegyek.SelectedItem.ToString();
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            using ( Stream mentes_fs = new FileStream("tantargyak.xml", FileMode.Create) )
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Tantargy>));
                serializer.Serialize(mentes_fs , tantargyak);
            }
            System.Windows.MessageBox.Show("Sikeres Mentés!");
        }
        private void Betoltes_Click(object sender, RoutedEventArgs e)
        {
            tantargyak = new List<Tantargy>();
            valasztottTargy = null;
            modositandoIndex = -1;
            targyvalaszto.Items.Clear();

            XmlSerializer deserializer = new XmlSerializer(typeof(List<Tantargy>));

            using ( FileStream betoltes_fs = File.OpenRead("tantargyak.xml") )
            {
                tantargyak = (List<Tantargy>) deserializer.Deserialize( betoltes_fs );
            }

            foreach (var item in tantargyak)
            {
                targyvalaszto.Items.Add( item.Nev );
            }
            System.Windows.MessageBox.Show("Sikeres Betöltést!");

        }
    }
}
