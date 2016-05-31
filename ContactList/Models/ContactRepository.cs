using ContactList.Utils;
using System.Collections.Generic;
using System.Linq;
using static ContactList.Services.Database;

namespace ContactList.Models {

    public class ContactRepository : IContactRepository {
        private IContactValidator Validator;

        public ContactRepository(IContactValidator validator) {
            //DB.DropTable<Contact>();
            DB.CreateTable<Contact>();
            Validator = validator;
        }

        public IList<Contact> GetContacts(string searchText = null) {
            var search = searchText ?? "";
            var scope = (from contact in DB.Table<Contact>()
                         where contact.Name.Contains(search)
                         select contact);

            return scope.OrderBy(c => c.Name, new CaseInsensitiveComparer()).ToList();
        }

        public CreateCodesEnum SaveContact(Contact contact) {
            if (!Validator.IsValid(contact)) return CreateCodesEnum.INVALID_DATA_ERROR;

            try {
                DB.Insert(contact);
            } catch (SQLite.Net.NotNullConstraintViolationException) {
                return CreateCodesEnum.NULL_NAME_ERROR;
            } catch (SQLite.Net.SQLiteException) {
                return CreateCodesEnum.DUPLICATE_NAME_ERROR;
            }

            return CreateCodesEnum.OK;
        }

        public bool DeleteContact(Contact contact) {
            return DB.Delete(contact) == 1;
        }

        public bool DeleteAll() {
            DB.DeleteAll<Contact>();
            return true;
        }

        public bool UpdateContact(Contact contact) {
            if (!Validator.IsValid(contact)) return false;

            return 1 == DB.Update(contact);
        }
    }
}