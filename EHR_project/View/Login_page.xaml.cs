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
using EHR_project;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Login_page.xaml
    /// </summary>
    public partial class Login_page : Page
    {
        public Login_page()
        {
            InitializeComponent();
        }
        private string nickname;
        private string pwd;
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {/*
            lblErrorLP.Content = ""; //nulovani erroru
            nickname = tbUsrName.Text;
            pwd = tbUsrPwd.Text;

            Dtbconnect dtb = new Dtbconnect();
            dtb.querry("SELECT id,name,surname FROM user_staff WHERE nickname='" + nickname + "' AND pwd='" + pwd + "';");
            MainWindow.string_list = dtb.returnvalue();
            if (MainWindow.string_list.Count > 0)
            {
                MainWindow.wmain.redirect_and_login();
            }
            else
            {
                lblErrorLP.Content = "Zadali jste špatné nebo neexistující identifikační údaje. Zkuste to prosím znovu.";
            }*/
        }
    }
}
