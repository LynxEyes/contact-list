using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.SampleData
{
    public class SampleContactData
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        public Contact CurrentContact { get; set; }

        public SampleContactData()
        {
            Contacts.Add(new Contact("Ze", null, null));
            Contacts.Add(new Contact("Carlos", null, null));
            Contacts.Add(new Contact("Alberto", null, null));
        }
    }
}
