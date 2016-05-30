using SQLite;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.Diagnostics;
using System.IO;
using Windows.Storage;

namespace ContactList.Services {
    public class Database {
        public static SQLiteConnection DB { get; } = new SQLiteConnection(new SQLitePlatformWinRT(), Path.Combine(ApplicationData.Current.LocalFolder.Path, "contacts.db"));

        static Database() {
            DB.TraceListener = new DebugTraceListener();
        }

        class DebugTraceListener : ITraceListener {
            public void Receive(string message) {
                Debug.WriteLine(message);
            }
        }
    }
}
