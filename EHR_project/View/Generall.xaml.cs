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
using EHR_project.Modul;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Generall.xaml
    /// </summary>
    public partial class Generall : Page
    {
        public Generall()
        {
            InitializeComponent();
        }
        private User user;
        public Generall(User user)
        {
            InitializeComponent();
            this.user = user;
            initialize_user();
        }

        public void initialize_user()
        {
            lbl_patient_name.Content = "Žádný";
            lbl_doc_name.Content = lbl_doc_name.Content + user.name + " " +user.surname;
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            user.logout_user(user.id);
            MainWindow.wmain.destroy_everything();
        }

        private void click_new_patient(object sender, RoutedEventArgs e)
        {
            MainWindow.wmain.Content = new Create_user();
        }
    }
}

