using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Models {
    public interface IContactValidator {
        bool IsValid(Contact contact);
    }
}
