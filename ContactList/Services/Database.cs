using SQLite;
using System.IO;
using Windows.Storage;

namespace ContactList.Services {
    public class Database {
        public static SQLiteConnection DB { get; } = new SQLiteConnection(Path.Combine(ApplicationData.Current.LocalFolder.Path, "contacts.db"));
    }
}
