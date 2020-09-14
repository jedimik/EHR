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
using EHR_project.Config;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using EHR_project.Modul;
using EHR_project.View.Patient_view;

namespace EHR_project
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        private MySqlDataReader reader;
        private Dtbconnect dtb;
        private int patient_examination_id; //Selectem z databaze
        private List<Medicine> medications = new List<Medicine>();
        string medication_history;
        Patient patient;
        public TestPage()
        {
            InitializeComponent();
            Get_medicine();
            tb_medication_history.Text = tb_medication_history.Text + DateTime.Now.ToString();
            
        }

        private void btn_save_medication_Click(object sender, RoutedEventArgs e)
        {
            foreach (var o  in lb_medication_choose.SelectedItems)
            {  //Napsani do textboxu
                tb_medication_history.Text = tb_medication_history.Text+ "\n" + o;

                foreach (Medicine medicine in medications)
                {
                    if (lb_medication_choose.Items.IndexOf(o)+1 == medicine.id)
                    { //tady bude insert podle indexu
                        Debug.Write(medicine.id);
                    }
                }
                

            }

            lb_medication_choose.SelectedItems.Clear();
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
            reader = dtb.Select("SELECT * FROM medication_link WHERE patientID='"+ patient.id +"';");
        
        }

        private void btn_back_to_anamnesis_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}



