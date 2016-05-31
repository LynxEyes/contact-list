using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUnitTests {

    public class BaseTest {

        static BaseTest() {
            Database.Platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();
            Database.DBPath = "contacts.db";
        }
    }
}