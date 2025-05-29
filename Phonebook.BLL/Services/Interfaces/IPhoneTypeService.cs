using Phonebook.DAL.Entities;

namespace Phonebook.BLL.Services;

public interface IPhoneTypeService
{
	Task<IEnumerable<PhoneType>> GetAllPhoneTypesAsync();
	Task<PhoneType> GetPhoneTypeByIdAsync(int id);
	Task<PhoneType> AddPhoneTypeAsync(PhoneType phoneType);
	Task DeletePhoneTypeAsync(int id);
}