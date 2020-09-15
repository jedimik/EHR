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
        //TODO predepsane leky z navstev
        Dtbconnect dtb;
        MySqlDataReader reader;
        Patient patient;
        private List<Medicine> medications = new List<Medicine>();
        private List<Medicine> choosen_medications = new List<Medicine>();
        private string return_medication_prescripted= "Pacientovi jsou předepsané následující léčiva: ";
        private int examinationID;
        private int interventionID;
        private int visitID;

        public Medication_list(Patient patient)
        {
            this.patient = patient;
            try //Kdyby se preklikl bez vysetreni
            {
                Get_Ids();
            }
            catch (Exception ex)
            {
                string messageBoxText = "Nejprve vyplnte a ulozte vysetreni.";
                string caption = "AIS";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            Debug.Write(patient.id);
            InitializeComponent();
            Get_medicine(); //NActeni leku
        }
        private void Get_medicine()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM medication;");
            int pocet = 0;
            while (reader.Read())
            {
                medications.Add(new Medicine(reader.GetInt32(0),pocet, reader.GetString(1), reader.GetInt32(2), false));
                lb_medication_choose.Items.Insert(pocet, reader.GetString(1));
                pocet = pocet + 1;
            }
            dtb = null;
        }

        private void Get_Ids()
        {
            this.examinationID = MainWindow.gen_main.anamnesis.exam.examination_id;
            this.interventionID = MainWindow.gen_main.anamnesis.exam.intervention_id;
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT id FROM visit WHERE examinationID='" + examinationID + "' AND patientID='" + patient.id + "';");
            while (reader.Read())
            {
                visitID = reader.GetInt32(0);
            }
        
        }

        private void btn_save_medication_Click(object sender, RoutedEventArgs e)
        {
            this.dtb = new Dtbconnect();
            foreach (var o in lb_medication_choose.SelectedItems)
            {  
                foreach (Medicine medicine in medications)
                {
                    if (lb_medication_choose.Items.IndexOf(o) == medicine.id_pocet)
                    {                        
                        choosen_medications.Add(new Medicine(medicine.id,lb_medication_choose.Items.IndexOf(o), medicine.name, medicine.sukl_code, true));
                        dtb.Insert("INSERT INTO medication_link (medicationID, visitID) VALUES('"+medicine.id+"','"+visitID+"');");
                        return_medication_prescripted += medicine.name+", ";
                    }
                }
            }
            lb_medication_choose.SelectedItems.Clear();
        }

        private void btn_back_to_examination_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.gen_main.anamnesis.exam.Add_to_Examination(return_medication_prescripted);
            MainWindow.wmain.Content = MainWindow.gen_main.anamnesis.exam;

        }
    }
}
