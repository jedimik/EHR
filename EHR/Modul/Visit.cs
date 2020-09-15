using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public  class Visit
    {
        public int id { get; set; }
        public int id_pocet { get; set; }
        public int examination_id { get; set; }
        public int intervention_id { get; set; }
        public int patient_id { get; set; }
        public DateTime date;

    public Visit(int id, int id_pocet, int examination_id, int intervention_id, int patient_id, DateTime date) 
    {
            this.id = id;
            this.id_pocet = id_pocet;
            this.examination_id = examination_id;
            this.intervention_id = intervention_id;
            this.patient_id = patient_id;
            this.date = date;
        
    }


    }
}
