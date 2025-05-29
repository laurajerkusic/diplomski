namespace MessageMicroservice.Models
{
	public class SendMessageModel
	{
		public int ContactId { get; set; }
		public string PhoneNumber { get; set; }
		public string Content { get; set; }
	}
}
