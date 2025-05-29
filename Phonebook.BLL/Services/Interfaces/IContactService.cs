using Phonebook.DAL.Entities;

namespace Phonebook.BLL.Services;

public interface IContactService
{
	Task<Contact> GetContactWithPhoneNumbersAsync(int id);
	Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm);
	Task<IEnumerable<Contact>> GetAllContactsAsync();
	Task<Contact> CreateContactAsync(Contact contact);
	Task UpdateContactAsync(int id, Contact contact);
	Task DeleteContactAsync(int id);
}