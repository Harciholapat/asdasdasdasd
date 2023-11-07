using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {            
        string filename = "organizations-100000.csv";

        List<Organization> organisations = new List<Organization>();
        List<string> orszaglista = new List<string>();
        List<string> evlista = new List<string>();

        

        public MainWindow()
        {
            InitializeComponent();
            Betolt();
            orgData.ItemsSource = organisations;
            OrszagTolt();
        }

        public void Betolt()
        {
            StreamReader reader = new StreamReader(filename);

            string headerLine = reader.ReadLine();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                Organization elem = new Organization(line.Split(';'));
                organisations.Add(elem);
            }
            reader.Close();
        }

        public void OrszagTolt()
        {
            organisations.GroupBy(x => x.Country).DistinctBy(x => x.Key).ToList().ForEach(x => CBcountryName.Items.Add(x.Key));
            organisations.GroupBy(x => x.Founded).DistinctBy(x => x.Key).ToList().ForEach(x => CBfoundingDate.Items.Add(x.Key));
        }

        private void orgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orgData.SelectedItem is Organization)
            {
                Organization valasztott = orgData.SelectedItem as Organization;
                azon.Content = valasztott.Id.ToString();
                web.Content = valasztott.Website;
                desc.Content = valasztott.Description;
            }
        }
    }
}
