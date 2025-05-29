using AutoMapper;
using Phonebook.API.DTO;
using Phonebook.DAL.Entities;

namespace Phonebook.API;

public class PhonebookProfile : Profile
{
	public PhonebookProfile()
	{
		CreateMap<Contact, ContactDTO>()
			.ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers));
		CreateMap<ContactRequestDTO, Contact>();

		CreateMap<PhoneNumber, PhoneNumberDTO>()
			.ForMember(dest => dest.PhoneTypeName, opt => opt.MapFrom(src => src.PhoneType != null ? src.PhoneType.TypeName : string.Empty));
		CreateMap<PhoneNumberRequestDTO, PhoneNumber>();

		CreateMap<PhoneType, PhoneTypeDTO>();
		CreateMap<PhoneTypeRequestDTO, PhoneType>();
	}
}