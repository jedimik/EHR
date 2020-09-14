using EHR_project.Config;
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
using MySql.Data.MySqlClient;
using EHR_project.View.Patient_view;
using System.Diagnostics;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Anamnesis.xaml
    /// </summary>
    public partial class Anamnesis : Page
    {
        Patient patient;
        private string anamnesis;
        Dtbconnect dtb;
        MySqlDataReader reader;
        string today;
        public Examination exam;
        Medication_list medication_list;
        public int examination_id { get; set; } //id exam pro leky
        public Anamnesis(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            lbl_patient_name.Content = MainWindow.gen_main.lbl_patient_name.Content;
            load_anamnesis();
            this.today = DateTime.Today.ToString("yyy/MM/dd");
            this.exam = new Examination(patient,today);
        }

        private void load_anamnesis()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT anamnesis FROM patient WHERE id='" + patient.id + "';");
            if (reader.Read())
            {
                anamnesis = reader.GetString(0);
            }
            if (patient.patient_anamnesis == false) //Jen jedno datum k jedne navsteve, moci dopisovat.
            {
                tb_anamnesis.Text = anamnesis;
                patient.patient_anamnesis = true;
            }
            else {
                tb_anamnesis.Text = anamnesis;
                    }
            dtb = null;
        }

        public void load_examination(string examination)
        {
            tb_anamnesis.Text = anamnesis + "\n" + examination + "\n";
        }

        public void Add_to_anamnesis(string content)
        {
            tb_anamnesis.Text = tb_anamnesis.Text +"\n" + content + "\n";
        }

        private void btn_back_to_generall_Click(object sender, RoutedEventArgs e)
        {
            Return_examination(exam); //Test
            MainWindow.wmain.Content = MainWindow.gen_main;
        }     

        private void btn_save_anamnesis_Click(object sender, RoutedEventArgs e)
        {
            anamnesis = tb_anamnesis.Text;
            this.dtb = new Dtbconnect();
            dtb.Update("UPDATE patient SET anamnesis='"+anamnesis+"' WHERE id='"+ patient.id+"';");
        }

        private void btn_examination_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = exam;
        }

        private void btn_medication_Click(object sender, RoutedEventArgs e)
        {
            this.medication_list = new Medication_list(patient);
            MainWindow.wmain.Content = medication_list;
        }
        public void Return_examination(Examination exam)
        {
            this.exam = exam;
        }

        public int Get_examination_id()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT id FROM examination WHERE date='" + today + "';");
            if (reader.Read())
            {
                examination_id = reader.GetInt32(0);
            }

            return examination_id;
        }
    }
}
