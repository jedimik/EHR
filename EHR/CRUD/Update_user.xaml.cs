using EHR_project.Modul;
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
using EHR_project.View;

namespace EHR_project.CRUD
{
    /// <summary>
    /// Interaction logic for Update_user.xaml
    /// </summary>
    public partial class Update_user : Page
    {
        private Patient patient;
        private MySqlDataReader reader;
        Dtbconnect dtb;
        public Update_user(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
            load_patient_data();
        }

        private void load_patient_data()
        {
            tb_name.Text = patient.name;
            tb_surname.Text = patient.surname;
            tb_tel_number.Text = patient.tel_number.ToString();
            tb_insurance.Text = patient.insurance_code.ToString();
            cb_sex.Items.Insert(0, patient.sex_name);
            tb_street_name.Text = patient.address_street_name;
            tb_house_number.Text = patient.address_street_number.ToString();
            tb_city.Text = patient.address_city;
            tb_psc.Text = patient.postal_code.ToString();
        }

        private void btn_back_to_generall_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = MainWindow.gen_main;
            MainWindow.gen_main.InitializeComponent();
        }

        private void btn_update_patient_Click(object sender, RoutedEventArgs e)
        {
            string cmd;
            dtb = new Dtbconnect();
            bool address_new = false;
            //Pokud se zmeni pacientovo udaje normal osobni
            if (tb_name.Text != patient.name || tb_surname.Text!=patient.surname || Int32.Parse(tb_tel_number.Text) != patient.tel_number)
            {
                cmd = "UPDATE patient " +
                    "SET name='" + tb_name.Text + "',surname='" + tb_surname.Text + "',tel_number='" + tb_tel_number.Text + "' " +
                    "WHERE id='" + patient.id + "';";
                dtb.Update(cmd);
                cmd = null;
            }
            // Pokud se zmeni adresa - udelat novou a pridat k pacientovi update address ID
            if (tb_street_name.Text != patient.address_street_name || Int32.Parse(tb_house_number.Text) != patient.address_street_number || tb_city.Text != patient.address_city || Int32.Parse(tb_psc.Text) != patient.postal_code)
            {
                reader = dtb.Select("SELECT address.id FROM address WHERE address.street_name='" + tb_street_name.Text + "' AND address.street_number='" + tb_house_number.Text + "' AND address.city='" + tb_city.Text + "' AND address.postal_code='" + tb_psc.Text+"';");
                if (reader.Read())
                {
                    patient.addressID = reader.GetInt32(0);
                    Debug.Write("Adresa 0=" + patient.addressID);//ffffff
                }
                else { address_new = true; }
                 
                if (address_new==true) //Pokud adresa neni v databazi - tak se vlozi nova, zjisti jeji ID a updatne patient
                {
                    cmd = "INSERT INTO address " +
                    "(street_name,street_number,city,postal_code) " +
                    "VALUES " +
                    "('" + tb_street_name.Text + "','" + tb_house_number.Text + "','" + tb_city.Text + "','" + tb_psc.Text + "');";
                    dtb.Insert(cmd);

                    //Zjisteni noveho id, Get_Last_ID nefungovalo
                    reader = dtb.Select("SELECT * FROM address WHERE city='" + tb_city.Text + "' AND street_name='" + tb_street_name.Text + "' AND street_number='" + tb_house_number.Text + "' AND postal_code='" + tb_street_name.Text + "';");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            patient.addressID = reader.GetInt32(0);
                            Debug.Write("Adresa 2=" + patient.addressID);//fffffff
                        }
                    }
                    cmd = null;
                    cmd = "UPDATE patient " +
                        "SET addressID='" + patient.addressID + "' " +
                        "WHERE id='" + patient.id + "';";
                    dtb.Update(cmd);
                    cmd = null;
                }
                else //ID existujici adresy priradim k pacientovi
                {
                    Debug.Write("Adresa 3=" + patient.addressID);//ffffff
                    cmd = "UPDATE patient " +
                        "SET addressID='" + patient.addressID + "' " +
                        "WHERE id='" + patient.id + "';";
                    dtb.Update(cmd);
                    cmd = null;
                }
            }
            //Pokud se zmeni pojisteni
            if (Int32.Parse(tb_insurance.Text) != patient.insurance_code)
            {
                cmd = "SELECT id, name FROM insurance WHERE code='" + tb_insurance.Text + "';";
                reader=dtb.Select(cmd);
                while (reader.Read())
                {
                    patient.insuranceID = reader.GetInt32(0);
                    patient.insurance_name = reader.GetString(1);
                    patient.insurance_code = Int32.Parse(tb_insurance.Text);
                }
                cmd = "UPDATE patient " +
                    "SET insuranceID='" + patient.insuranceID + "'" +
                    "WHERE id='" + patient.id + "';"; ;
                dtb.Update(cmd);
                cmd = null;
            }


            
            string messageBoxText = "Úprava dat uložena.";
            string caption = "AIS";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}

