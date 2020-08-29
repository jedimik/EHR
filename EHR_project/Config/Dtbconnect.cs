using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace EHR_project.Config
{
    class Dtbconnect
    {
        static string Mysqlconn = "Server=localhost;Database=ehr;Uid=ehr;Pwd=ehr;";
        //List<String> string_list = new List<String>();
        public MySqlConnection connection;
        public MySqlDataReader querry(string command)
        {
            this.connection = new MySqlConnection(Mysqlconn);
            MySqlCommand cmd = new MySqlCommand(command, connection);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public void closeConn()
        {
            this.connection.Close();
        }
        /*
        public void querry(string command)
        {
            MySqlConnection connection = new MySqlConnection(Mysqlconn);
            MySqlCommand cmd = new MySqlCommand(command, connection);
            connection.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {                
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string_list.Add(reader[i].ToString());
                }
            }
            connection.Close();
        }
        public List<String> returnvalue()
        {
            return string_list;
        }*/
    }
}
