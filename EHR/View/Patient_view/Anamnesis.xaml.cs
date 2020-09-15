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
        public Examination_concrete examination_concrete;
        public Medicine medicine;
        public Intervention intervention;
        public Patient patient;
        Dtbconnect dtb;
        public MySqlDataReader reader;
        public Examination exam;
        public Exam_history exam_history;

        string today;
        private string anamnesis;
        public int examination_id { get; set; } //id exam pro leky
        public Anamnesis(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            lbl_patient_name.Content = MainWindow.gen_main.lbl_patient_name.Content;
            load_anamnesis();
            this.exam = new Examination(patient);
        }

        private void load_anamnesis()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT examination.today_examination " +
                "FROM visit " +
                "INNER JOIN examination on visit.examinationID=examination.id " +
                "WHERE patientID='"+patient.id+"';");
            while (reader.Read())
            {
                anamnesis = reader.GetString(0);
                tb_anamnesis.Text += anamnesis + "\n\n";
            }

        }

        private void btn_back_to_generall_Click(object sender, RoutedEventArgs e)
        {
            Return_examination(exam); //Test
            MainWindow.wmain.Content = MainWindow.gen_main;
        }     

        private void btn_examination_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = exam;
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

        private void btn_history_exam_Click(object sender, RoutedEventArgs e)
        {
            this.exam_history = new Exam_history(patient);
           MainWindow.wmain.Content = exam_history;

        }
    }
}
