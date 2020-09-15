using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public class Intervention
    {
        public int id { get; set; }
        public int id_pocet { get; set; }
        public string name { get; set; }
        public string snomed_code { get; set; }
        public string loinc_code { get; set; }
        public string mkn10_code { get; set; }

        public Intervention(int id, int id_pocet, string name, string snomed_code, string loinc_code, string mkn10_code)
        {
            this.id = id;
            this.id_pocet = id_pocet;
            this.name = name;
            this.snomed_code = snomed_code;
            this.loinc_code = loinc_code;
            this.mkn10_code = mkn10_code;
        }

    }
}
