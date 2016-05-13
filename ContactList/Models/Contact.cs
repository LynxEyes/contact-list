using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Models {
    public class Contact {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set;  }

        public Contact(string Name, string Email, string Mobile) {
            this.Email = Email;
            this.Name = Name;
            this.Mobile = Mobile;
        }
    }
}
