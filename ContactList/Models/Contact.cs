﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Models {
    public class Contact {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set;  }

        public Contact() : this(null, null, null) { }

        public Contact(string name) : this(name, null, null) { }

        public Contact(string name, string email, string mobile) {
            Email = email;
            Name = name;
            Mobile = mobile;
        }
    }
}