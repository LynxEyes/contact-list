using SQLite.Net;
using SQLite.Net.Interop;
using System.Diagnostics;
using System.IO;
using Windows.Storage;

namespace Common.Services {

    public class Database {
        public static ISQLitePlatform Platform { get; set; }
        public static string DBPath { get; set; }
        //public static SQLiteConnection DB { get; } = new SQLiteConnection(new SQLitePlatformGeneric(), Path.Combine(ApplicationData.Current.LocalFolder.Path, "contacts.db"));

        private static SQLiteConnection db;

        public static SQLiteConnection DB {
            get {
                if (db == null) {
                    db = new SQLiteConnection(Platform, DBPath); // Path.Combine(ApplicationData.Current.LocalFolder.Path, "contacts.db"));
#if DEBUG
                    db.TraceListener = new DebugTraceListener();
#endif
                }

                return db;
            }
        }

        private class DebugTraceListener : ITraceListener {

            public void Receive(string message) {
                Debug.WriteLine(message);
            }
        }
    }
}