﻿using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ContactManager.Models
{
    public class EntityContactManagerRepository : ContactManager.Models.IContactManagerRepository
    {
        private ContactManagerDBEntities _entities = new ContactManagerDBEntities();
        public Contact GetContact(int id)
        {
            return (from c in _entities.ContactSet
                    where c.ID == id
                    select c).FirstOrDefault();
        }

        public IEnumerable<Contact> ListContacts()
        {
            return _entities.ContactSet.ToList();
        }

        public Contact CreateContact(Contact contactToCreate)
        {
            _entities.AddToContactSet(contactToCreate);
            _entities.SaveChanges();
            return contactToCreate;
        }

        public Contact EditContact(Contact contactToEdit)
        {
            var originalContact = GetContact(contactToEdit.ID);
            _entities.ApplyPropertyChanges(originalContact.EntityKey.EntitySetName, contactToEdit);
            _entities.SaveChanges();
            return contactToEdit;
        }

        public void DeleteContact(Contact contactToDelete)
        {
            var originalContact = GetContact(contactToDelete.ID);
            _entities.DeleteObject(originalContact);
            _entities.SaveChanges();
        }
    }
}
