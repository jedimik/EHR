using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    class Address
    {
        private string street_name { get; set; }
        private int street_number { get; set; }
        private string city { get; set; }
        private int postal_code { get; set; }
        public Address(string street_name,int street_number, string city, int postal_code)
        {
            this.street_name = street_name;
            this.street_number = street_number;
            this.city = city;
            this.postal_code = postal_code;
        }


    }
}
