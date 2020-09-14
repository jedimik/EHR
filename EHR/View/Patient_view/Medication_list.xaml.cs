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
using EHR_project.Config;
using EHR_project.Modul;
using System.Diagnostics;

namespace EHR_project.View.Patient_view
{
    /// <summary>
    /// Interaction logic for Medication_list.xaml
    /// </summary>
public partial class Medication_list : Page
    {
        Dtbconnect dtb;
        MySqlDataReader reader;
        Patient patient;
        private List<Medicine> medications = new List<Medicine>();
        private List<Medicine> choosen_medications = new List<Medicine>();
        private string medication_history;
        private string return_medication_prescripted= "Pacientovi jsou předepsané následující léčiva: ";
        private int examination_id;

        public Medication_list(Patient patient)
        {
            try //Kdyby se preklikl bez vysetreni
            {
                this.examination_id = MainWindow.gen_main.anamnesis.Get_examination_id();                
            }
            catch (Exception ex)
            {
            }
            this.patient = patient;
            InitializeComponent();
            Get_medicine(); //NActeni leku
            Get_medication_history(); //NActeni minulych leku pacienta
            Debug.Write(examination_id);
        }
        private void Get_medicine()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM medication;");
            while (reader.Read())
            {
                medications.Add(new Medicine(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), false));

            }

            foreach (var o in medications)
            {
                lb_medication_choose.Items.Insert((o.id - 1), o.name); //-1 aby to bylo od nuly                 
            }
            dtb = null;
        }
        private void Get_medication_history()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT medication.name, examination.date" +
                " FROM medication_link" +
                " INNER JOIN medication ON medication_link.medicationID = medication.ID" +
                " INNER JOIN examination ON medication_link.examinationID = examination.ID " +
                " WHERE medication_link.patientID='" + patient.id + "';");
            int pocet = 0;
            while (reader.Read())
            {
                if (pocet == 0)
                {
                    medication_history = reader.GetDateTime(1) + "\n" + reader.GetString(0);
                }
                else
                {
                    medication_history += "\n" + reader.GetString(0);
                }
                pocet++;
            }
            tb_medication_history.Text = tb_medication_history.Text + medication_history +"\n\nDNES: "+ DateTime.Now.ToString(); ;
        }
        private void btn_save_medication_Click(object sender, RoutedEventArgs e)
        {
            this.dtb = new Dtbconnect();
            foreach (var o in lb_medication_choose.SelectedItems)
            {  //Napsani do textboxu
                tb_medication_history.Text = tb_medication_history.Text + "\n" + o;
                foreach (Medicine medicine in medications)
                {
                    if (lb_medication_choose.Items.IndexOf(o) + 1 == medicine.id)
                    {                        
                        choosen_medications.Add(new Medicine(lb_medication_choose.Items.IndexOf(o), medicine.name, medicine.sukl_code, true));
                        //Dani do databaze
                        dtb.Insert("INSERT INTO medication_link (patientID, medicationID, examinationID) VALUES('" + patient.id + "','" + medicine.id + "','" + examination_id + "');");
                        return_medication_prescripted += medicine.name+", ";
                    }
                }
            }
            lb_medication_choose.SelectedItems.Clear();
        }

        private void btn_back_to_anamnesis_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.gen_main.anamnesis.Add_to_anamnesis(return_medication_prescripted);
            MainWindow.wmain.Content = MainWindow.gen_main.anamnesis;

        }

    }
}
