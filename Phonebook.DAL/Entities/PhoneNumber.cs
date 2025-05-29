namespace Phonebook.DAL.Entities
{
	public class PhoneNumber
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public bool IsMain { get; set; }
		public int ContactId { get; set; }
		public Contact Contact { get; set; }
		public int? PhoneTypeId { get; set; }
		public PhoneType? PhoneType { get; set; }
	}
}