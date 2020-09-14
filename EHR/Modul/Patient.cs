using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public class Patient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int sexID { get; set; }
        public string sex_name { get; set; }
        public int addressID { get; set; }
        public string address_street_name { get; set; }
        public int address_street_number{ get; set; }
        public string address_city { get; set; }
        public int postal_code { get; set; }
        public int tel_number { get; set; }
        public int insuranceID { get; set; }
        public int insurance_code { get; set; }
        public string insurance_name { get; set; }
        public bool patient_anamnesis { get; set; } //JEstli se uz psala pacientova anamneza kvuli datumu

        public Patient(int id, string name, string surname, int sexID, string sex_name, int addressID, string address_street_name, int address_street_number,
            string address_city, int postal_code, int tel_number, int insuranceID, int insurance_code, string insurance_name, bool patient_anamnesis)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.sexID = sexID;
            this.sex_name = sex_name;
            this.addressID = addressID;
            this.address_street_name = address_street_name;
            this.address_street_number = address_street_number;
            this.address_city = address_city;
            this.postal_code = postal_code;
            this.tel_number = tel_number;
            this.insuranceID = insuranceID;
            this.insurance_code = insurance_code;
            this.insurance_name = insurance_name;
            this.patient_anamnesis = patient_anamnesis;
        }



    }
}
