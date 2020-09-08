using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    class Patient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int sexID { get; set; }
        public int addressID { get; set; }
        public int tel_number { get; set; }
        public int insuranceID { get; set; }

        public Patient(int id, string name, string surname, int sexID, int addressID, int tel_number, int insuranceID)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.sexID = sexID;
            this.addressID = addressID;
            this.tel_number = tel_number;
            this.insuranceID = insuranceID;
        }




    }
}
