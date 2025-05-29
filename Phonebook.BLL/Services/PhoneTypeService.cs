using Phonebook.BLL.Exceptions;
using Phonebook.DAL.Entities;
using Phonebook.DAL.Repositories;

namespace Phonebook.BLL.Services;

public class PhoneTypeService : IPhoneTypeService
{
	private readonly IRepository<PhoneType> _phoneTypeRepository;

	public PhoneTypeService(IRepository<PhoneType> phoneTypeRepository)
	{
		_phoneTypeRepository = phoneTypeRepository;
	}

	public async Task<IEnumerable<PhoneType>> GetAllPhoneTypesAsync()
	{
		return await _phoneTypeRepository.GetAllAsync();
	}

	public async Task<PhoneType> GetPhoneTypeByIdAsync(int id)
	{
		var phoneType = await _phoneTypeRepository.GetByIdAsync(id);
		if (phoneType == null)
		{
			throw new NotFoundException($"Phone type ID {id} not found.");
		}
		return phoneType;
	}

	public async Task<PhoneType> AddPhoneTypeAsync(PhoneType phoneType)
	{
		if (phoneType == null)
		{
			throw new ArgumentNullException(nameof(phoneType));
		}

		if (string.IsNullOrWhiteSpace(phoneType.TypeName))
		{
			throw new ArgumentException("Phone type name should not be empty.", nameof(phoneType.TypeName));
		}

		await _phoneTypeRepository.AddAsync(phoneType);
		await _phoneTypeRepository.SaveChangesAsync();
		return phoneType;
	}

	public async Task DeletePhoneTypeAsync(int id)
	{
		var phoneType = await _phoneTypeRepository.GetByIdAsync(id);
		if (phoneType == null)
		{
			throw new NotFoundException($"Phone type ID {id} not found.");
		}

		_phoneTypeRepository.Remove(phoneType);
		await _phoneTypeRepository.SaveChangesAsync();
	}
}