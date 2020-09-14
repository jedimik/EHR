using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using EHR_project.Modul;
using MySql.Data.MySqlClient;

namespace EHR_project.View.Patient_view
{
    /// <summary>
    /// Interaction logic for Examination.xaml
    /// </summary>
    public partial class Examination : Page
    {
        Patient patient;
        Patient_examination patient_exam;
        private string examination="";
        private int examination_code; //predelat
        private string examination_text;
        Dtbconnect dtb;
        MySqlDataReader reader;
        string today;
        List<Examination_concrete> examinations = new List<Examination_concrete>(); //seznam

        public Examination(Patient patient, string today)
        {
            this.patient=patient;
            this.today = today;
            InitializeComponent();
            LoadExaminations();
        }

        private void LoadExaminations()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM examination_codes");
            int pocet = 0;
            while (reader.Read())
            {
                examinations.Add(new Examination_concrete(pocet,reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4))) ;
                pocet=pocet+1;
            }
            foreach (var o in examinations)
            {
                lb_diagnosis.Items.Insert(o.idpocet, o.name); //-1 aby to bylo od nuly                 
            }
            dtb = null;
        }

        private void btn_save_examination_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var o in lb_diagnosis.SelectedItems)
                {  //Napsani do textboxu
                    foreach (Examination_concrete exc in examinations)
                    {
                        if (lb_diagnosis.Items.IndexOf(o)  == exc.idpocet)
                        {
                            examination_code = exc.id;
                        }
                    }
                }
                this.patient_exam = new Patient_examination(patient.id, Double.Parse(tb_weight.Text), Double.Parse(tb_height.Text), Int32.Parse(tb_pressure_dis.Text),
                    Int32.Parse(tb_pressure_sys.Text), Int32.Parse(tb_saturation.Text), Int32.Parse(tb_bpm.Text), today, examination, examination_code);
                examination_text = tb_examination.Text;
                double BMI = Double.Parse(tb_weight.Text) / (Double.Parse(tb_height.Text) / 100 * Double.Parse(tb_height.Text) / 100);
                examination = "Dne:" + patient_exam.date + " Váha=" + patient_exam.weight + " Výška=" + patient_exam.height + " BMI= " + BMI.ToString() + "\n" +
                    "Diastolický tlak= " + patient_exam.pressure_dis + " Systolický tlak= " + patient_exam.pressure_sys + " Saturace= " + patient_exam.saturation +
                    "\nVyšetření:" + examination_text;
                patient_exam.examination = examination;
                MainWindow.gen_main.anamnesis.Add_to_anamnesis(examination);
                this.dtb = new Dtbconnect();
                Debug.Write(patient_exam.date);
                dtb.Insert("INSERT INTO examination (patientID, weight, height, pressure_dis, pressure_sys, saturation" +
                    ", BPM, date, today_examination, exam_code_ID)" +
                    " VALUES ('" + patient.id + "','" + patient_exam.weight + "','" + patient_exam.height + "','" + patient_exam.pressure_dis + "','" + patient_exam.pressure_sys + "'," +
                    "'" + patient_exam.saturation + "','" + patient_exam.bpm + "','" + DateTime.Today.ToString("yyy/MM/dd") +"','" + patient_exam.examination + "','" + examination_code + "');");                         
            }
            catch (Exception ex)
            {
                string messageBoxText = "Překontrolujte prosím údaje. Všechna pole musí být vyplněna.";
                string caption = "AIS";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }           
        }
        private void btn_back_to_anamnesis_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = MainWindow.gen_main.anamnesis;
        }





    }
}
