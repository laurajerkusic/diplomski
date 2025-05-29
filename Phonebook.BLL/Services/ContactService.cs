using Phonebook.BLL.Exceptions;
using Phonebook.DAL.Entities;
using Phonebook.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.BLL.Services;

public class ContactService : IContactService
{
	private readonly IPhonebookRepository _phonebookRepository;

	public ContactService(IPhonebookRepository phonebookRepository)
	{
		_phonebookRepository = phonebookRepository;
	}

	public async Task<Contact> GetContactWithPhoneNumbersAsync(int id)
	{
		var contact = await _phonebookRepository.GetWithPhoneNumbersAsync(id);
		if (contact == null)
		{
			throw new NotFoundException($"Contact ID {id} not found.");
		}
		return contact;
	}

	public async Task<IEnumerable<Contact>> GetAllContactsAsync()
	{
		return await _phonebookRepository.GetAll()
			.Include(c => c.PhoneNumbers)
			.ThenInclude(pn => pn.PhoneType)
			.ToListAsync();
	}

	public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
	{
		if (string.IsNullOrWhiteSpace(searchTerm))
		{
			throw new ArgumentException("Search term must not be empty.", nameof(searchTerm));
		}
		return await _phonebookRepository.SearchContactsAsync(searchTerm);
	}

	public async Task<Contact> CreateContactAsync(Contact contact)
	{
		if (contact == null)
		{
			throw new ArgumentNullException(nameof(contact));
		}

		if (string.IsNullOrWhiteSpace(contact.FirstName) || string.IsNullOrWhiteSpace(contact.LastName))
		{
			throw new ArgumentException("First name and Last name are required fields.");
		}

		await _phonebookRepository.AddAsync(contact);
		await _phonebookRepository.SaveChangesAsync();
		return contact;
	}

	public async Task UpdateContactAsync(int id, Contact contact)
	{
		if (contact == null)
		{
			throw new ArgumentNullException(nameof(contact));
		}

		var existingContact = await _phonebookRepository.GetByIdAsync(id);
		if (existingContact == null)
		{
			throw new NotFoundException($"Contact ID {id} not found.");
		}

		existingContact.FirstName = contact.FirstName;
		existingContact.LastName = contact.LastName;
		existingContact.Address = contact.Address;
		existingContact.Email = contact.Email;

		await _phonebookRepository.UpdateAsync(existingContact);
		await _phonebookRepository.SaveChangesAsync();
	}

	public async Task DeleteContactAsync(int id)
	{
		var contact = await _phonebookRepository.GetByIdAsync(id);
		if (contact == null)
		{
			throw new NotFoundException($"Contact ID {id} not found.");
		}

		_phonebookRepository.Remove(contact);
		await _phonebookRepository.SaveChangesAsync();
	}
}