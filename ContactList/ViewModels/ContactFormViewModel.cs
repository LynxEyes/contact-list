using ContactList.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;

namespace ContactList.ViewModels {
    public abstract class ContactFormViewModel : ViewModelBase, INotifyPropertyChanged {
        public ICommand SaveContactCommand => new RelayCommand(SaveContact);

        public abstract void SaveContact();

        public new event PropertyChangedEventHandler PropertyChanged;

        public string Name {
            get { return Contact?.Name; }
            set { Contact.Name = value; }
        }

        public string Email {
            get { return Contact?.Email; }
            set { Contact.Email = value; }
        }

        public string Mobile {
            get { return Contact?.Mobile; }
            set { Contact.Mobile = value; }
        }

        private Contact contact = new Contact();
        public Contact Contact {
            get { return contact; }
            protected set {
                contact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contact"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mobile"));
            }
        }

        private string contactFormTitle;
        public string ContactFormTitle {
            get { return contactFormTitle; }
            protected set {
                contactFormTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ContactFormTitle"));
            }
        }

        private string errorMessage;
        public string ErrorMessage {
            get { return errorMessage; }
            protected set {
                errorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
        }
    }
}
