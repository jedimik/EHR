using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public class Medicine
    {
        public int id { get; set; }
        public int sukl_code { get; set; }
        public string name { get; set; }

        public bool choosen { get; set; }
        public Medicine(int id, string name, int sukl_code, bool choosen)
        {
            this.id = id;
            this.name = name;
            this.sukl_code = sukl_code;
            this.choosen = choosen;
        }

    }
}
