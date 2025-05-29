using Phonebook.DAL.Entities;

namespace Phonebook.DAL.Repositories;

public interface IPhonebookRepository : IRepository<Contact>
{
	Task<Contact?> GetWithPhoneNumbersAsync(int id);
	Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm);
}