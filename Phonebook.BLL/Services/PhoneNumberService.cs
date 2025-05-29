using Phonebook.BLL.Exceptions;
using Phonebook.DAL.Entities;
using Phonebook.DAL.Repositories;

namespace Phonebook.BLL.Services;

public class PhoneNumberService : IPhoneNumberService
{
	private readonly IPhoneNumberRepository _phoneNumberRepository;
	private readonly IPhonebookRepository _phonebookRepository;

	public PhoneNumberService(IPhoneNumberRepository phoneNumberRepository, IPhonebookRepository phonebookRepository)
	{
		_phoneNumberRepository = phoneNumberRepository;
		_phonebookRepository = phonebookRepository;
	}

	public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByContactIdAsync(int contactId)
	{
		var contact = await _phonebookRepository.GetByIdAsync(contactId);
		if (contact == null)
		{
			throw new NotFoundException($"Contact ID {contactId} not found.");
		}

		return await _phoneNumberRepository.GetPhoneNumbersByContactIdAsync(contactId);
	}

	public async Task<PhoneNumber> AddPhoneNumberAsync(int contactId, PhoneNumber phoneNumber)
	{
		if (phoneNumber == null)
		{
			throw new ArgumentNullException(nameof(phoneNumber));
		}

		var contact = await _phonebookRepository.GetByIdAsync(contactId);
		if (contact == null)
		{
			throw new NotFoundException($"Contact ID {contactId} not found.");
		}

		if (string.IsNullOrWhiteSpace(phoneNumber.Number))
		{
			throw new ArgumentException("Telephone number may not be empty.", nameof(phoneNumber.Number));
		}

		if (phoneNumber.IsMain)
		{
			var existingPhoneNumbers = await _phoneNumberRepository.GetPhoneNumbersByContactIdAsync(contactId);
			foreach (var existingNumber in existingPhoneNumbers.Where(pn => pn.IsMain))
			{
				existingNumber.IsMain = false;
				await _phoneNumberRepository.UpdateAsync(existingNumber);
			}
		}

		phoneNumber.ContactId = contactId;
		await _phoneNumberRepository.AddAsync(phoneNumber);
		await _phoneNumberRepository.SaveChangesAsync();
		return phoneNumber;
	}

	public async Task UpdatePhoneNumberAsync(int phoneNumberId, PhoneNumber phoneNumber, int contactId)
	{
		if (phoneNumber == null)
		{
			throw new ArgumentNullException(nameof(phoneNumber));
		}

		var existingPhoneNumber = await _phoneNumberRepository.GetByIdAsync(phoneNumberId);
		if (existingPhoneNumber == null)
		{
			throw new NotFoundException($"Telephone number ID {phoneNumberId} not found.");
		}

		existingPhoneNumber.Number = phoneNumber.Number;
		existingPhoneNumber.PhoneTypeId = phoneNumber.PhoneTypeId;

		if (phoneNumber.IsMain)
		{
			var existingPhoneNumbers = await _phoneNumberRepository.GetPhoneNumbersByContactIdAsync(contactId);
			foreach (var existingNumber in existingPhoneNumbers.Where(pn => pn.Id != phoneNumberId && pn.IsMain))
			{
				existingNumber.IsMain = false;
				await _phoneNumberRepository.UpdateAsync(existingNumber);
			}
			existingPhoneNumber.IsMain = true;
		}
		else
		{
			existingPhoneNumber.IsMain = false;
		}

		await _phoneNumberRepository.UpdateAsync(existingPhoneNumber);
		await _phoneNumberRepository.SaveChangesAsync();
	}

	public async Task DeletePhoneNumberAsync(int phoneNumberId)
	{
		var phoneNumber = await _phoneNumberRepository.GetByIdAsync(phoneNumberId);
		if (phoneNumber == null)
		{
			throw new NotFoundException($"Telephone number ID {phoneNumberId} not found.");
		}

		_phoneNumberRepository.Remove(phoneNumber);
		await _phoneNumberRepository.SaveChangesAsync();
	}

	public async Task SetDefaultNumberAsync(int contactId, int phoneNumberId)
	{
		var contact = await _phonebookRepository.GetByIdAsync(contactId);
		if (contact == null)
		{
			throw new NotFoundException($"Contact ID  {contactId}  not found.");
		}

		var phoneNumber = await _phoneNumberRepository.GetByIdAsync(phoneNumberId);
		if (phoneNumber == null)
		{
			throw new NotFoundException($"Telephone number ID {phoneNumberId} not found.");
		}

		if (phoneNumber.ContactId != contactId)
		{
			throw new ArgumentException($"Telephone number ID {phoneNumberId} is not assigned to contact ID {contactId}.");
		}

		await _phoneNumberRepository.SetDefaultNumberAsync(contactId, phoneNumberId);
	}
}