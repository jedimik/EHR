using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_project.Modul
{
    public class User
    {
        public string name { get; set; }
        public int id { get; set; }
        public string surname { get; set; }
        public bool logged { get; set; }
        public User() { }
        public User(int id, string name, string surname)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            logged = true;
        }
        public void logout_user(int id)
        {
            logged = false;
        }

    }
}
