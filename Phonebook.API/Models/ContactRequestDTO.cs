namespace Phonebook.API.DTO;

public class ContactRequestDTO
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public string? Email { get; set; }
}