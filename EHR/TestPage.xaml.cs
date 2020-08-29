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
using EHR_project.Config;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace EHR_project
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
           /* this.dtb = new Dtbconnect();
            dtb.querry("SELECT * FROM sex");
            string_list = dtb.returnvalue();
            for (int i = 0; i < string_list.Count; i++)
            {
                // Debug.Write(string_list.Count);
            }*/
        }
        //public static List<String> string_list { get; set; }
        private MySqlDataReader reader;
        private void register_click(object sender, RoutedEventArgs e)
        {
            Dtbconnect dtb = new Dtbconnect();
            string querry = "SELECT * FROM sex";
            reader = dtb.querry(querry);
            int pocet = 0;
            while (reader.Read())
            {
                cb_sex.Items.Insert(pocet, reader.GetString(1));
                pocet++;
            }
            dtb.closeConn();
        }
    }
}
