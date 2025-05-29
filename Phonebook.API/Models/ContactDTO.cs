namespace Phonebook.API.DTO;

public class ContactDTO
{
	public int Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public string? Email { get; set; }
	public List<PhoneNumberDTO> PhoneNumbers { get; set; } = new List<PhoneNumberDTO>();
}