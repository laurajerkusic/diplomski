using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Data;
using Phonebook.DAL.Entities;

namespace Phonebook.DAL.Repositories;

public class PhonebookRepository : Repository<Contact>, IPhonebookRepository
{
	public PhonebookRepository(PhonebookDbContext context) : base(context) { }

	public async Task<Contact?> GetWithPhoneNumbersAsync(int id)
	{
		return await GetAll()
			.Include(c => c.PhoneNumbers)
			.ThenInclude(p => p.PhoneType)
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
	{
		return await GetAll()
			.Where(c =>
				c.FirstName.Contains(searchTerm) ||
				c.LastName.Contains(searchTerm) ||
				c.PhoneNumbers.Any(p => p.Number.Contains(searchTerm)))
			.ToListAsync();
	}
}