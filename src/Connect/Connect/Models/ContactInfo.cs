namespace Connect.Models {

    public class ContactInfo {

        /// <summary>
        /// The contact's position in the business.
        /// </summary>
        public string Title {
            get;
            set;
        }

        /// <summary>
        /// The full name of the contact.
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// The phone number for the contact.
        /// </summary>
        public string Phone {
            get;
            set;
        }

        /// <summary>
        /// The email address for the contact.
        /// </summary>
        public string Email {
            get;
            set;
        }

        public ContactInfo() { }

        public ContactInfo(string title, string name, string phone, string email) {
            Title = title;
            Name  = name;
            Phone = phone;
            Email = email;
        }
    }
}