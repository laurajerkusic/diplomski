namespace MessageMicroservice.Models
{
	public class Message
	{
		public int Id { get; set; }
		public int ContactId { get; set; }
		public string PhoneNumber { get; set; }
		public string Content { get; set; }
		public DateTime SentDateTime { get; set; }
	}
}
