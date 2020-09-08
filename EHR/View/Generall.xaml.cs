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
using EHR_project.Modul;
using EHR_project.Config;
using MySql.Data.MySqlClient;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Generall.xaml
    /// </summary>
    public partial class Generall : Page
    {
        public Generall()
        {
            InitializeComponent();          
        }
        private MySqlDataReader reader;
        Dictionary<int, string> patients = new Dictionary<int, string>();
        private User user;
        private Patient patient;
        public int patient_id;
        public Generall(User user)
        {
            InitializeComponent();
            this.user = user;
            initialize_user();
        }        

        public void initialize_user()
        {
            lbl_patient_name.Content = lbl_patient_name.Content+"Žádný";
            lbl_doc_name.Content = lbl_doc_name.Content + user.name + " " +user.surname;
            
            Dtbconnect dtb = new Dtbconnect();
            reader = dtb.Select("SELECT id, name, surname FROM patient");
            int pocet = 0;
            while (reader.Read())
            {   
                patients.Add(reader.GetInt32(0), reader.GetString(1) + " " + reader.GetString(2));
                cb_patients.Items.Insert(pocet, reader.GetString(1) + " " + reader.GetString(2));             
                pocet++;
            }
          
            dtb.CloseConn();
            dtb = null;
        }

        private void getID(string name_surname) //Ziskat Id podle dictionary pro select pacienta
        {
            foreach (KeyValuePair<int, string> pair in patients)
            {
                if (pair.Value == name_surname)
                {
                    this.patient_id = pair.Key;
                }
            }            
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.destroy_everything();
        }

        private void click_new_patient(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = new Create_user();
        }

        private void btn_logout_patient_Click(object sender, RoutedEventArgs e)
        {
            lbl_patient_name.Content = "Aktuální pacient:";
            patient = null;
            cb_patients.Visibility = Visibility.Visible;
            btn_final_choose_patient.Visibility = Visibility.Visible;
            lbl_pick_patient.Visibility = Visibility.Visible;
        }

        private void btn_final_choose_patient_Click(object sender, RoutedEventArgs e)
        {
            getID(cb_patients.SelectedItem.ToString());
            Dtbconnect dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM patient WHERE id='"+patient_id+"';");
            while (reader.Read())
            {   
                this.patient = new Patient(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetInt32(3),reader.GetInt32(4),reader.GetInt32(5),reader.GetInt32(7));
            }
            dtb.CloseConn();
            dtb = null;
            lbl_patient_name.Content = "Aktuální pacient:" + patient.name +" "+ patient.surname;
            cb_patients.Visibility = Visibility.Hidden;
            btn_final_choose_patient.Visibility = Visibility.Hidden;
            lbl_pick_patient.Visibility = Visibility.Hidden;
        }
    }
}

