using Phonebook.DAL.Entities;

namespace Phonebook.BLL.Services;

public interface IPhoneNumberService
{
	Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByContactIdAsync(int contactId);
	Task<PhoneNumber> AddPhoneNumberAsync(int contactId, PhoneNumber phoneNumber);
	Task UpdatePhoneNumberAsync(int phoneNumberId, PhoneNumber phoneNumber, int contactId);
	Task DeletePhoneNumberAsync(int phoneNumberId);
	Task SetDefaultNumberAsync(int contactId, int phoneNumberId);
}