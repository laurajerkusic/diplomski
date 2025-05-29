using System.Collections.Generic;

namespace Phonebook.DAL.Entities
{
	public class Contact
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
	}
}