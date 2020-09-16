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
using EHR_project.Modul;
using EHR_project.Config;
using System.Diagnostics;

namespace EHR_project.View.Patient_view
{
    /// <summary>
    /// Interaction logic for Exam_history.xaml
    /// </summary>
    public partial class Exam_history : Page
    {
        Patient patient;
        Dtbconnect dtb;
        MySqlDataReader reader;
        Visit visit;
        Patient_examination patient_examination;
        Prescripted_medicine prescripted_medicine;
        List<Visit> visits = new List<Visit>();
        List<Examination_concrete> examinations = new List<Examination_concrete>(); //seznam exam
        List<Intervention> interventions = new List<Intervention>(); //seznam interv

        private string cmd;
        int ex_id;
        int int_id;
        string examination_text;
        string intervention_name;
        string examination_name;
        public Exam_history(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            Load_dates();            
        }

        private void Load_dates()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM visit WHERE patientID='"+patient.id+"';");
            int pocet = 0;
            while (reader.Read())
            {
               visits.Add(new Visit(reader.GetInt32(0),pocet, reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4)));
                pocet = pocet + 1;
            }
            foreach (var o in visits)
            {
                lb_visits.Items.Insert(o.id_pocet, o.date);
            }
            dtb = null;
        }


        private void btn_choose_date_Click(object sender, RoutedEventArgs e) //Nacteni minule navstevy
        {
            //Get selected ID 
            foreach (var o in lb_visits.SelectedItems)
            {  
                foreach (Visit vis in visits)
                {
                    if (lb_visits.Items.IndexOf(o) == vis.id_pocet)
                    {
                        this.visit = new Visit(vis.id,0,vis.examination_id,vis.intervention_id,vis.patient_id,vis.date);
                    }
                }
            }

            this.dtb = new Dtbconnect();
            //Examination data
            reader = dtb.Select("SELECT * FROM examination WHERE id='" + visit.examination_id + "';");
            while (reader.Read())
            {
                this.patient_examination = new Patient_examination(reader.GetDouble(1),reader.GetDouble(2),reader.GetInt32(3),reader.GetInt32(4),reader.GetInt32(5)
                    ,reader.GetInt32(6),reader.GetString(7),reader.GetInt32(8));
            }
            tb_weight.Text = patient_examination.weight.ToString();
            tb_height.Text = patient_examination.height.ToString();
            tb_pressure_sys.Text = patient_examination.pressure_sys.ToString();
            tb_pressure_dis.Text = patient_examination.pressure_dis.ToString();
            tb_saturation.Text = patient_examination.saturation.ToString();
            tb_bpm.Text = patient_examination.bpm.ToString();
            tb_examination.Text = patient_examination.examination;
            LoadInterventions();
            LoadExaminations(); 
            foreach (Examination_concrete exc in examinations)
            {
                if (exc.id == visit.examination_id)
                {
                    lb_diagnosis.SelectedItem = lb_diagnosis.Items.GetItemAt(exc.idpocet);
                }
            }
            foreach (Intervention inter in interventions)
            {
                if (inter.id == visit.intervention_id)
                {
                   lb_intervence.SelectedItem = lb_intervence.Items.GetItemAt(inter.id_pocet);
                }
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (var o in lb_visits.SelectedItems)
            {
                foreach (Visit vis in visits)
                {
                    if (lb_visits.Items.IndexOf(o) == vis.id_pocet)
                    {
                        this.visit = new Visit(vis.id, 0, vis.examination_id, vis.intervention_id, vis.patient_id, vis.date);
                    }
                }
            }

            this.dtb = new Dtbconnect();
            dtb.Delete("DELETE FROM medication_link WHERE visitID='" + visit.id + "';");
            dtb.Delete("DELETE FROM examination WHERE id='" + visit.examination_id + "';");
            dtb.Delete("DELETE FROM visit WHERE id='" + visit.id + "';");
            string messageBoxText = "Zaznam vymazan.";
            string caption = "AIS";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
            dtb = null;
            visit = null;
            
        }

        private void btn_save_examination_Click(object sender, RoutedEventArgs e)
        {
           try
            {//Pro TB a vysetreni
                if (Double.Parse(tb_weight.Text) != patient_examination.weight || Double.Parse(tb_height.Text) != patient_examination.height ||
                    Int32.Parse(tb_pressure_sys.Text) != patient_examination.pressure_sys || Int32.Parse(tb_pressure_dis.Text) != patient_examination.pressure_dis ||
                    Int32.Parse(tb_saturation.Text) != patient_examination.saturation || Int32.Parse(tb_bpm.Text) != patient_examination.bpm ||
                    tb_examination.Text != patient_examination.examination)
                {
                double BMI = Double.Parse(tb_weight.Text) / (Double.Parse(tb_height.Text) / 100 * Double.Parse(tb_height.Text) / 100);

                patient_examination.examination = "Dne:" + visit.date + " Váha=" + tb_weight.Text + " Výška=" + tb_height.Text + " BMI= " + BMI.ToString() + "\n" +
                   "Diastolický tlak= " + tb_pressure_dis.Text + " Systolický tlak= " + tb_pressure_sys.Text + " Saturace= " + tb_saturation.Text + "BPM:"+ tb_bpm.Text +
                   "\nIntervence:" + intervention_name + " Diagnoza:"+ examination_name+ "\nVyšetření:" + tb_examination.Text;

                this.dtb = new Dtbconnect();
                    cmd = "UPDATE examination " +
                       "SET weight='" + tb_weight.Text + "',height='" + tb_height.Text + "',pressure_dis='" + tb_pressure_dis.Text + "',pressure_sys='" + tb_pressure_sys.Text + "'," +
                       "saturation='" + tb_saturation.Text + "',BPM='" + tb_bpm.Text + "',today_examination='" + patient_examination.examination + "'" +
                       " WHERE id='" + visit.examination_id + "';";
                    dtb.Update(cmd);
                }
                //Pro listboxy
                //Diagnozy
                foreach (var o in lb_diagnosis.SelectedItems)
                {  //Napsani do textboxu
                    foreach (Examination_concrete exc in examinations)
                    {
                        if (lb_diagnosis.Items.IndexOf(o) == exc.idpocet)
                        {
                            ex_id = exc.id;
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
                            int_id = inter.id;
                        intervention_name = inter.name;
                        }
                    }
                }
            
            if (ex_id != patient_examination.examination_code)
            {
                this.dtb = new Dtbconnect();
                dtb.Update("UPDATE examination SET exam_code_ID='" + ex_id + "' WHERE examination.id='"+ visit.examination_id+"';");
            }
            if (int_id != visit.intervention_id)
            {
                this.dtb = new Dtbconnect();
                dtb.Update("UPDATE visit SET interventionID='" + int_id + "' WHERE examinationID='"+visit.examination_id+"';");
            }

              string messageBoxText = "Data byla uspesne zmenena.";
              string caption = "AIS";
              MessageBoxButton button = MessageBoxButton.OK;
              MessageBoxImage icon = MessageBoxImage.Information;
              MessageBox.Show(messageBoxText, caption, button, icon);
          }
          catch (Exception ex)
          {
              string messageBoxText = "Prekontrolujte prosim data, nekde se stala chyba.";
              string caption = "AIS";
              MessageBoxButton button = MessageBoxButton.OK;
              MessageBoxImage icon = MessageBoxImage.Warning;
              MessageBox.Show(messageBoxText, caption, button, icon);
          }
        }

        private void btn_medication_Click(object sender, RoutedEventArgs e)
        {
            this.prescripted_medicine = new Prescripted_medicine(visit,patient);
            MainWindow.wmain.Content = prescripted_medicine;

        }

        private void btn_back_to_anamnesis_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = MainWindow.gen_main.anamnesis;
        }

        public void Add_to_examination(string command)
        {
            patient_examination.examination += command; 
        }
        private void LoadExaminations()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM examination_codes");
            int pocet = 0;
            while (reader.Read())
            {
                examinations.Add(new Examination_concrete(pocet, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                pocet = pocet + 1;
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
    }
}
