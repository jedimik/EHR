using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    class Patient
    {
        private string name { get; set; }
        private string surname { get; set; }
        private int sexID { get; set; }
        private int addressID { get; set; }
        private int tel_number { get; set; }
        private int insuranceID { get; set; }

        public Patient(string name, string surname, int sexID, int addressID, int tel_number, int insuranceID)
        {
            this.name = name;
            this.surname = surname;
            this.sexID = sexID;
            this.addressID = addressID;
            this.tel_number = tel_number;
            this.insuranceID = insuranceID;
        }




    }
}
