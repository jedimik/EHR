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
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
        private MySqlDataReader reader;
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            lblErrorLP.Content = ""; //nulovani erroru
            nickname = tbUsrName.Text;
            pwd = tbUsrPwd.Password;

            Dtbconnect dtb = new Dtbconnect();
            string cmd = "SELECT id,name,surname FROM user_staff WHERE nickname='" + nickname + "' AND pwd='" + pwd + "';";
            this.reader =dtb.Select(cmd);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MainWindow.wmain.string_list.Insert(0,reader.GetInt16(0).ToString());
                    MainWindow.wmain.string_list.Insert(1,reader.GetString(1));
                    MainWindow.wmain.string_list.Insert(2,reader.GetString(2));                    
                }
                dtb.CloseConn();
                MainWindow.wmain.redirect_and_login();
            }
            else
            {
                lblErrorLP.Content = "Zadali jste špatné nebo neexistující identifikační údaje. Zkuste to prosím znovu.";
            }
        }
    }
}
