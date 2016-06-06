using SQLite.Net;
using SQLite.Net.Interop;
using System.Diagnostics;

namespace Common.Services {

    public class Database {
        public static ISQLitePlatform Platform { get; set; }
        public static string DBPath { get; set; }

        private static SQLiteConnection db;

        public static SQLiteConnection DB {
            get {
                if (db == null) {
                    db = new SQLiteConnection(Platform, DBPath);
#if DEBUG
                    db.TraceListener = new DebugTraceListener();
#endif
                }

                return db;
            }
        }

        private class DebugTraceListener : ITraceListener {

            public void Receive(string message) => Debug.WriteLine(message);
        }
    }
}