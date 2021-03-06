﻿using System;
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
using EHR_project.CRUD;
using MySql.Data.MySqlClient;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Generall.xaml
    /// </summary>
    public partial class Generall : Page
    {
        private MySqlDataReader reader;
        Dictionary<int, string> patients = new Dictionary<int, string>();
        private User user;
        private Patient patient;
        public Anamnesis anamnesis;
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

        private void btn_logout_Click(object sender, RoutedEventArgs e) //Odlaseni lekare
        {
            MainWindow.wmain.destroy_everything();
        }
        private void btn_new_patient_Click(object sender, RoutedEventArgs e) //ODhlaseni pacienta
        {
            MainWindow.wmain.Content = new Create_user();
        }
        private void btn_logout_patient_Click(object sender, RoutedEventArgs e)
        {
            lbl_patient_name.Content = "Aktuální pacient:";
            patient = null;
            //Visibility buttonu po logout
            cb_patients.Visibility = Visibility.Visible;
            btn_final_choose_patient.Visibility = Visibility.Visible;
            lbl_pick_patient.Visibility = Visibility.Visible;
            btn_anamnesis.Visibility = Visibility.Hidden;
            btn_update_patient.Visibility = Visibility.Hidden;
        }

        private void btn_final_choose_patient_Click(object sender, RoutedEventArgs e)
        {
            getID(cb_patients.SelectedItem.ToString());
            Dtbconnect dtb = new Dtbconnect();
            reader = dtb.Select("SELECT " +
            "patient.name, patient.surname, " +
            "sex.ID, sex.name, " +
            "address.ID, address.street_name, address.street_number, address.city, address.postal_code, " +
            "patient.tel_number," +
            "insurance.ID, insurance.code, insurance.name " +
            "FROM patient " +
            "INNER join sex on sex.id=sexID " +
            "INNER join address on address.ID=addressID " +
            "INNER join insurance on insurance.ID=insuranceID " +
            "WHERE patient.ID='" + patient_id + "';");
            while (reader.Read())
            {
                this.patient = new Patient(patient_id,reader.GetString(0),reader.GetString(1),
                    reader.GetInt32(2),reader.GetString(3),
                    reader.GetInt32(4), reader.GetString(5),reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8),
                    reader.GetInt32(9),
                    reader.GetInt32(10),reader.GetInt32(11), reader.GetString(12),false);
            }
            dtb.CloseConn();
            dtb = null;
            this.anamnesis = new Anamnesis(patient); //Aby byla jednou nacetla po loginu
            lbl_patient_name.Content = "Aktuální pacient:" + patient.name +" "+ patient.surname;
            //Visibility buttonu po login
            cb_patients.Visibility = Visibility.Hidden;
            btn_final_choose_patient.Visibility = Visibility.Hidden;
            lbl_pick_patient.Visibility = Visibility.Hidden;
            btn_update_patient.Visibility = Visibility.Visible;
            btn_anamnesis.Visibility = Visibility.Visible;
        }

        private void btn_update_patient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = new Update_user(patient);
        }

        private void btn_anamnesis_Click(object sender, RoutedEventArgs e)            
        {            
            MainWindow.wmain.Content = anamnesis;
        }
        //Test jestli zlepsi
        public void Return_anamnesis(Anamnesis anamnesis)
        {
            this.anamnesis = anamnesis;
        }
        public void Return_patient(Patient patient)
        {
            this.patient=patient;
        }
        public void Return_user(User user)
        {
            this.user=user;
        }

    }
}

