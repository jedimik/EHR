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
using EHR_project.Config;
using EHR_project.Modul;
using System.Diagnostics;

namespace EHR_project.View.Patient_view
{
    public partial class Prescripted_medicine : Page
    {
        Dtbconnect dtb;
        MySqlDataReader reader;
        Patient patient;
        Visit visit;
        List<int> indexes = new List<int>();
        private List<Medicine> medications = new List<Medicine>();
        private List<Medicine> choosen_medications = new List<Medicine>();
        private List<Medicine> prescripted_medication = new List<Medicine>();
        private string return_medication_prescripted = "Pacientovi jsou předepsané následující léčiva: ";
        public Prescripted_medicine(Visit visit, Patient patient)
        {
            this.patient = patient;
            this.visit = visit;
            InitializeComponent();
            LoadMedicine();
            LoadHistory();
        }

        private void LoadMedicine()
        {
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT * FROM medication;");
            int pocet = 0;
            while (reader.Read())
            {
                medications.Add(new Medicine(reader.GetInt32(0),pocet, reader.GetString(1), reader.GetInt32(2), false));
                pocet = pocet + 1;
            }

            foreach (var o in medications)
            {
                lb_medication_choose.Items.Insert(o.id_pocet, o.name);               
            }
            dtb = null;
        }

        private void LoadHistory()
        {
            lb_prescripted_medicine.Items.Clear();
            this.dtb = new Dtbconnect();
            reader = dtb.Select("SELECT medication.ID, medication.name " +
                "FROM medication_link " +
                "INNER JOIN medication on medication_link.medicationID = medication.ID " +
                "WHERE visitID='" + visit.id + "';");
            int pocet = 0;
            while (reader.Read())
            {
                prescripted_medication.Add(new Medicine(reader.GetInt32(0), pocet, reader.GetString(1), 0, false));
                lb_prescripted_medicine.Items.Insert(pocet,reader.GetString(1));
                pocet = pocet + 1;
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
                        dtb.Insert("INSERT INTO medication_link (medicationID, visitID) VALUES('" + medicine.id + "','" + visit.id + "');");
                    }
                }
            }
            LoadHistory();
        }

        private void btn_delete_medication_Click(object sender, RoutedEventArgs e)
        {
            this.dtb = new Dtbconnect();
            foreach (var o in lb_prescripted_medicine.SelectedItems)
            {  
                foreach (Medicine medicine in  prescripted_medication)
                {
                    if (lb_prescripted_medicine.Items.IndexOf(o) == medicine.id_pocet)
                    {
                        indexes.Add(lb_prescripted_medicine.Items.IndexOf(o));                      
                        dtb.Delete("DELETE FROM medication_link WHERE visitID='"+visit.id+"' AND medicationID='"+medicine.id+"';");

                    }
                }
            }
            for (int i = 0; i < indexes.Count; i++)
            {
                lb_prescripted_medicine.Items.RemoveAt(indexes[i]);
            }
            indexes.Clear();
            string messageBoxText = "Leky uspesne smazany";
            string caption = "AIS";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void btn_back_to_examination_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.gen_main.anamnesis.exam_history.Add_to_examination(return_medication_prescripted);
            MainWindow.wmain.Content = MainWindow.gen_main.anamnesis.exam_history;
        }
    }
}
