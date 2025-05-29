using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phonebook.API.DTO;
using Phonebook.BLL.Services;
using Phonebook.DAL.Entities;

namespace Phonebook.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContactsController : ControllerBase
{
	private readonly IContactService _contactService;
	private readonly IMapper _mapper;

	public ContactsController(IContactService contactService, IMapper mapper)
	{
		_contactService = contactService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ContactDTO>>> GetAllContacts()
	{
		var contacts = await _contactService.GetAllContactsAsync();
		return Ok(_mapper.Map<IEnumerable<ContactDTO>>(contacts));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ContactDTO>> GetContact(int id)
	{
		var contact = await _contactService.GetContactWithPhoneNumbersAsync(id);
		return Ok(_mapper.Map<ContactDTO>(contact));
	}

	[HttpGet("search")]
	public async Task<ActionResult<IEnumerable<ContactDTO>>> SearchContacts([FromQuery] string searchTerm)
	{
		var contacts = await _contactService.SearchContactsAsync(searchTerm);
		return Ok(_mapper.Map<IEnumerable<ContactDTO>>(contacts));
	}

	[HttpPost]
	public async Task<ActionResult<ContactDTO>> CreateContact([FromBody] ContactRequestDTO contactRequest)
	{
		var contact = _mapper.Map<Contact>(contactRequest);
		var createdContact = await _contactService.CreateContactAsync(contact);
		var contactDto = _mapper.Map<ContactDTO>(createdContact);
		return CreatedAtAction(nameof(GetContact), new { id = contactDto.Id }, contactDto);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactRequestDTO contactRequest)
	{
		var contact = _mapper.Map<Contact>(contactRequest);
		await _contactService.UpdateContactAsync(id, contact);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteContact(int id)
	{
		await _contactService.DeleteContactAsync(id);
		return NoContent();
	}
}