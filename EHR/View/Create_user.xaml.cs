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
using System.Diagnostics;
using EHR_project.Config;
using MySql.Data.MySqlClient;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Create_user.xaml
    /// </summary>
    public partial class Create_user : Page
    {
        private MySqlDataReader reader;
        private Dtbconnect dtb;
        int address_id = 0;
        int sex_id = 0;
        int insurance_id = 0;
        Dictionary<int, string> insurance = new Dictionary<int, string>();
        public Create_user()
        {
            InitializeComponent();
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM sex");
            int pocet = 0;
            while (reader.Read())
            {
                cb_sex.Items.Insert(pocet, reader.GetString(1));
                pocet++;
            }
            dtb.CloseConn();
            dtb = null;
        }
        private void register_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Get_ids(); //Nastaveni ID pro sex a Insurance
                string cmd;
                this.dtb = new Dtbconnect();
                reader = dtb.Select("SELECT * FROM address WHERE city='" + tb_city.Text + "' AND street_name='" + tb_street_name.Text + "' AND street_number='" + tb_house_number.Text + "';");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        address_id = reader.GetInt32(0);
                    }
                }
                else
                {
                    cmd = @"INSERT INTO address
                (street_name,street_number,city,postal_code)
                VALUES
                ('" + tb_street_name.Text + "','" + tb_house_number.Text + "','" + tb_city.Text + "','" + tb_psc.Text + "');";
                    dtb.Insert(cmd);
                    this.dtb = new Dtbconnect();
                    reader = dtb.Select("SELECT * FROM address WHERE city='" + tb_city.Text + "' AND street_name='" + tb_street_name.Text + "' AND street_number='" + tb_house_number.Text + "';");
                    while (reader.Read())
                    {
                        address_id = reader.GetInt32(0);
                    }
                }
                cmd = @"INSERT INTO patient 
                    (name,surname,sexID,addressID,tel_number,insuranceID)
                    VALUES
                ('" + tb_name.Text + "','" + tb_surname.Text + "','" + sex_id + "','" + address_id + "','" + tb_tel_number.Text + "','" + insurance_id + "');";

                dtb.Insert(cmd);
                string messageBoxText = "Registrace byla úspěšně provedena";
                string caption = "AIS";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.None;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            catch (Exception ex)
            {
                string messageBoxText = "Překontrolujte prosím údaje. Registrace nebyla provedena";
                string caption = "AIS";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            insurance.Clear(); //vycisteni pojisteni
        }

        private void Get_ids()
        {
            sex_id = cb_sex.SelectedIndex + 1;//je to od nuly a databaze ID od jedne
            insurance.Add(1, "111");
            insurance.Add(2, "201");
            insurance.Add(3, "205");
            foreach (KeyValuePair<int, string> pair in insurance)
            {
                if (pair.Value == tb_insurance.Text)
                {
                    this.insurance_id = pair.Key;
                }
            }
        }

        private void btn_back_to_generall_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content=MainWindow.gen_main;
        }
    }
}