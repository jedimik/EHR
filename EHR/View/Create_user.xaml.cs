using EHR_project.Config;
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
using System.Diagnostics;

namespace EHR_project.View
{
    /// <summary>
    /// Interaction logic for Create_user.xaml
    /// </summary>
    public partial class Create_user : Page
    {
        public Create_user()
        {
            InitializeComponent();
            this.dtb = new Dtbconnect();
           /* dtb.querry("SELECT * FROM sex"); ZRUSIT PAK
            string_list = dtb.returnvalue();
            for (int i = 0; i < string_list.Count; i++)
            {
               // Debug.Write(string_list.Count);
            }*/
        }
        public static List<String> string_list { get; set; }
        private Dtbconnect dtb;
        
        private void register_click(object sender, RoutedEventArgs e)
        {
           // Debug.Write(string_list[0]);
           // Debug.Write(string_list[1]);
        }
        
    }
}
