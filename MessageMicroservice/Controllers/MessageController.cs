using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageMicroservice.Data;
using MessageMicroservice.Models;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : ControllerBase
{
	private readonly MessageDbContext _context;

	public MessageController(MessageDbContext context)
	{
		_context = context;
	}

	[HttpPost("send")]
	public async Task<IActionResult> SendMessage([FromBody] SendMessageModel model)
	{
		if (string.IsNullOrEmpty(model.Content) || model.Content.Length > 160)
			return BadRequest("Message must be between 1 and 160 characters.");

		var message = new Message
		{
			ContactId = model.ContactId,
			PhoneNumber = model.PhoneNumber,
			Content = model.Content,
			SentDateTime = DateTime.UtcNow
		};

		_context.Messages.Add(message);
		await _context.SaveChangesAsync();

		return Ok(new { Message = "Message sent successfully" });
	}

	[HttpGet("messages/{contactId}")]
	public async Task<IActionResult> GetMessages(int contactId)
	{
		var messages = await _context.Messages
			.Where(m => m.ContactId == contactId)
			.Select(m => new
			{
				m.PhoneNumber,
				m.Content,
				m.SentDateTime
			})
			.ToListAsync();

		return Ok(messages);
	}
}