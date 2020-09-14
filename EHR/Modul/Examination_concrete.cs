using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public class Examination_concrete //Examination codes
    {
        public int id { get; set; }

        public int idpocet { get; set; }
        public string name { get; set; }
        public string loinc_code { get; set; }
        public string snomed_code { get; set; }
        public string mkn10_code { get; set; }

        public Examination_concrete(int idpocet, int id, string name, string loinc_code, string snomed_code, string mkn10_code)
        {
            this.idpocet=idpocet;
            this.id = id;
            this.name = name;
            this.loinc_code = loinc_code;
            this.snomed_code = snomed_code;
            this.mkn10_code = mkn10_code;
        }
    }
}
