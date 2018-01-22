using System.Collections.Generic;

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

        /// <summary>
        /// Gets a collection of <see cref="ContactInfo"/>s from a <see cref="ProjectDetails"/>.
        /// </summary>
        /// <param name="projectDetails">The project details containing contact info.</param>
        /// <returns>A collection of contacts.</returns>
        /// <remarks>
        /// TODO: The APIs should return a ProjectDetails model with a collection of ContactInfo object instead of using individual properties on the model itself.
        /// </remarks>
        public static List<ContactInfo> GetContacts(ProjectDetails projectDetails) {
            List<ContactInfo> contacts = new List<ContactInfo>();

            if(!string.IsNullOrEmpty(projectDetails.projectDirector)) {
                contacts.Add(new ContactInfo("Project Director", projectDetails.projectDirector, projectDetails.directorPhone, projectDetails.directorEmail));
            }

            if(!string.IsNullOrEmpty(projectDetails.projectLead)) {
                contacts.Add(new ContactInfo("Project Lead", projectDetails.projectLead, projectDetails.leadPhone, projectDetails.leadEmail));
            }

            if(!string.IsNullOrEmpty(projectDetails.leadDataManager)) {
                contacts.Add(new ContactInfo("Lead Data Manager", projectDetails.leadDataManager, projectDetails.leadDmPhone, projectDetails.leadDmEmail));
            }

            return contacts;
        }
    }
}