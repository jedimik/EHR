﻿using System;
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
using System.Windows.Shapes;
using EHR_project.View;
using EHR_project.Modul;
using EHR_project.CRUD;
using System.Diagnostics;

namespace EHR_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow wmain;
        public MainWindow()
        {
            InitializeComponent();
            wmain = this;
            load_base();
        }
        private static User user_main { get; set; }
        public static Generall gen_main { get; set; }
        private  Login_page lp_main { get; set; }
        public  List<String> string_list = new List<String>(); //Vracene hodnoty
        public void load_base()
        {
            //Pro testovani
           // MainW.Content = new TestPage();
            //Main
            lp_main = new Login_page();
            MainW.Content = lp_main;
        }

        public void redirect_and_login()
        {
            user_main = new User(Int32.Parse(string_list[0]), string_list[1], string_list[2]);
            gen_main = new Generall(user_main);
            MainW.Content = gen_main;
        }
        public void destroy_everything()
        {       
            lp_main = null;
            user_main = null;
            gen_main = null;
            wmain.Hide();
            wmain=new MainWindow();
            wmain.Show();
        }

    }
}
