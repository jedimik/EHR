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
        Medication_list medication_list;
        private string examination="";
        private int examination_code; //predelat
        private string examination_text;
        public int intervention_id { get; set; }
        public int examination_id { get; set; }
        private string intervention_name;
        private string examination_name;
        DateTime datetime;
        Dtbconnect dtb;
        MySqlDataReader reader;
        List<Examination_concrete> examinations = new List<Examination_concrete>(); //seznam exam
        List<Intervention> interventions = new List<Intervention>(); //seznam interv

        public Examination(Patient patient)
        {
            this.patient=patient;
            InitializeComponent();
            LoadExaminations();
            LoadInterventions();
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
                lb_diagnosis.Items.Insert(o.idpocet, o.name);                  
            }
            dtb = null;
        }

        private void LoadInterventions()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM intervention");
            int pocet = 0;
            while (reader.Read())
            {
                interventions.Add(new Intervention(reader.GetInt32(0), pocet, reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                pocet = pocet + 1;
            }
            foreach (var o in interventions)
            {
                lb_intervence.Items.Insert(o.id_pocet, o.name);                 
            }
            dtb = null;
        }

        private void btn_save_examination_Click(object sender, RoutedEventArgs e)
        {
            try
            {   //Diagnozy
                foreach (var o in lb_diagnosis.SelectedItems)
                {  //Napsani do textboxu
                    foreach (Examination_concrete exc in examinations)
                    {
                        if (lb_diagnosis.Items.IndexOf(o)  == exc.idpocet)
                        {
                            examination_code = exc.id;
                            examination_name = exc.name;
                        }
                    }
                }
                //Intervence
                foreach (var o in lb_intervence.SelectedItems)
                {  //Napsani do textboxu
                    foreach (Intervention inter in interventions)
                    {
                        if (lb_intervence.Items.IndexOf(o) == inter.id_pocet)
                        {
                            intervention_id = inter.id;
                            intervention_name = inter.name;
                        }
                    }
                }

                var date= Convert.ToDateTime(dp_date.Text).ToString("yyyy/MM/dd");
                datetime = dp_date.SelectedDate.Value;
                this.patient_exam = new Patient_examination(Double.Parse(tb_weight.Text), Double.Parse(tb_height.Text), Int32.Parse(tb_pressure_dis.Text),
                    Int32.Parse(tb_pressure_sys.Text), Int32.Parse(tb_saturation.Text), Int32.Parse(tb_bpm.Text), examination, examination_code);
                examination_text = tb_examination.Text;

                double BMI = Double.Parse(tb_weight.Text) / (Double.Parse(tb_height.Text) / 100 * Double.Parse(tb_height.Text) / 100);
                //Text na vysetreni
                examination = "Dne:"+ datetime  + " Váha=" + patient_exam.weight + " Výška=" + patient_exam.height + " BMI= " + BMI.ToString() + "\n" +
                    "Diastolický tlak= " + patient_exam.pressure_dis + " Systolický tlak= " + patient_exam.pressure_sys + " Saturace= " + patient_exam.saturation +
                    "\nIntervence: "+intervention_name+ " Diagnoza: "+examination_name+"\nVyšetření: " + examination_text;

                patient_exam.examination = examination;
                this.dtb = new Dtbconnect();
                dtb.Insert("INSERT INTO examination (weight, height, pressure_dis, pressure_sys, saturation" +
                    ", BPM, today_examination, exam_code_ID)" +
                    " VALUES ('" + patient_exam.weight + "','" + patient_exam.height + "','" + patient_exam.pressure_dis + "','" + patient_exam.pressure_sys + "'," +
                    "'" + patient_exam.saturation + "','" + patient_exam.bpm  + "','" + patient_exam.examination + "','" + examination_code + "');");                             
                //Nefunguje get Last inserted ID proto tak osklive
                reader=dtb.Select("SELECT id FROM examination WHERE weight='"+ patient_exam.weight +"' AND height='"+ patient_exam.height+"' AND pressure_dis='"+ patient_exam.pressure_dis+"'" +
                    " AND pressure_sys='"+ patient_exam.pressure_sys+"' AND saturation='"+ patient_exam.saturation+"' AND BPM='"+patient_exam.bpm+"' AND " +
                    "today_examination='"+ patient_exam.examination+ "' AND exam_code_ID='"+ examination_code +"';");
                while (reader.Read())
                {
                    examination_id = reader.GetInt32(0);
                }
                dtb.Insert("INSERT INTO visit (examinationID, interventionID, patientID, date) " +
                    "VALUES ('" + examination_id + "','" + intervention_id + "','" + patient.id + "','" + date + "')");

                string messageBoxText = "Vysetreni ulozeno.";
                string caption = "AIS";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
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

        private void btn_medication_Click(object sender, RoutedEventArgs e)
        {
            this.medication_list = new Medication_list(patient);
            MainWindow.wmain.Content = medication_list;
        }
        public void Add_to_Examination(string content)
        {
            string help="";
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT today_examination FROM examination WHERE id='" + examination_id + "';");
            while (reader.Read())
            {
                help = reader.GetString(0);
            }
            help += content;
            dtb.Update("UPDATE examination SET today_examination='" + help + "' WHERE id='" + examination_id + "';");            
        }
    }
}
