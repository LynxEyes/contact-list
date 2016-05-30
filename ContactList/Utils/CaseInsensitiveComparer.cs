using System;
using System.Collections.Generic;

namespace ContactList.Utils {

    public class CaseInsensitiveComparer : IComparer<string> {

        public int Compare(string x, string y) {
            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }
    }
}