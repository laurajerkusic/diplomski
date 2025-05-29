using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Data;
using Phonebook.DAL.Entities;

namespace Phonebook.DAL.Repositories;

public class PhoneNumberRepository : Repository<PhoneNumber>, IPhoneNumberRepository
{
	public PhoneNumberRepository(PhonebookDbContext context) : base(context) { }

	public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByContactIdAsync(int contactId)
	{
		return await GetAll()
			.Where(pn => pn.ContactId == contactId)
			.Include(pn => pn.PhoneType)
			.ToListAsync();
	}

	public async Task SetDefaultNumberAsync(int contactId, int phoneNumberId)
	{
		var filteredPhoneNumbers = await GetAll()
			.Where(p => p.ContactId == contactId)
			.ToListAsync();

		foreach (var phoneNumber in filteredPhoneNumbers)
		{
			phoneNumber.IsMain = (phoneNumber.Id == phoneNumberId);
			await UpdateAsync(phoneNumber);
		}

		await SaveChangesAsync();
	}
}