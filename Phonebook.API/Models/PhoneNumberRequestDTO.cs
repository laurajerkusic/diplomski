namespace Phonebook.API.DTO;

public class PhoneNumberRequestDTO
{
	public string Number { get; set; } = string.Empty;
	public bool IsMain { get; set; }
	public int PhoneTypeId { get; set; }
	public int ContactId { get; set; }
}