﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public class Patient_examination
    {
        public int patientID { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public int pressure_dis { get; set; }
        public int pressure_sys { get; set; }
        public int saturation { get; set; }
        public int bpm { get; set; }
        public string date { get; set; }
        public string examination { get; set; }   
        
        public int examination_code { get; set; }

        public Patient_examination( int patientID, double weight, double height, int pressure_dis, int pressure_sys, int saturation, int bpm, string date, string examination, int examination_code)
        {
            this.patientID = patientID;
            this.weight = weight;
            this.height = height;
            this.pressure_dis = pressure_dis;
            this.pressure_sys = pressure_sys;
            this.saturation = saturation;
            this.bpm = bpm;
            this.date = date;
            this.examination = examination;
            this.examination_code = examination_code;
        }


    }
}