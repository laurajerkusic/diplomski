using Phonebook.DAL.Entities;

namespace Phonebook.DAL.Repositories;

public interface IPhoneNumberRepository : IRepository<PhoneNumber>
{
	Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByContactIdAsync(int contactId);
	Task SetDefaultNumberAsync(int contactId, int phoneNumberId);
}