using System;

namespace Common.Utils {

    public static class StringExtensions {

        public static bool IsEmpty(this String str) {
            return (str == null || str.Length == 0);
        }
    }
}