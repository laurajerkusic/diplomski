namespace Phonebook.API.DTO;

public class PhoneNumberDTO
{
	public int Id { get; set; }
	public string Number { get; set; } = string.Empty;
	public bool IsMain { get; set; }
	public int PhoneTypeId { get; set; }
	public string PhoneTypeName { get; set; } = string.Empty;
	public int ContactId { get; set; }
}