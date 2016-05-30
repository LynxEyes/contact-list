namespace ContactList.Models {

    public interface IContactValidator {

        bool IsValid(Contact contact);
    }
}