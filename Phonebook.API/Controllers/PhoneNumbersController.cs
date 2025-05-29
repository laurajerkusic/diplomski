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
public class PhoneNumbersController : ControllerBase
{
	private readonly IPhoneNumberService _phoneNumberService;
	private readonly IMapper _mapper;

	public PhoneNumbersController(IPhoneNumberService phoneNumberService, IMapper mapper)
	{
		_phoneNumberService = phoneNumberService;
		_mapper = mapper;
	}

	[HttpGet("contact/{contactId}")]
	public async Task<ActionResult<IEnumerable<PhoneNumberDTO>>> GetPhoneNumbersByContactId(int contactId)
	{
		var phoneNumbers = await _phoneNumberService.GetPhoneNumbersByContactIdAsync(contactId);
		return Ok(_mapper.Map<IEnumerable<PhoneNumberDTO>>(phoneNumbers));
	}

	[HttpPost("contact/{contactId}")]
	public async Task<ActionResult<PhoneNumberDTO>> AddPhoneNumber(int contactId, [FromBody] PhoneNumberRequestDTO phoneNumberRequest)
	{
		var phoneNumber = _mapper.Map<PhoneNumber>(phoneNumberRequest);
		var createdPhoneNumber = await _phoneNumberService.AddPhoneNumberAsync(contactId, phoneNumber);
		var phoneNumberDto = _mapper.Map<PhoneNumberDTO>(createdPhoneNumber);
		return CreatedAtAction(nameof(GetPhoneNumbersByContactId), new { contactId }, phoneNumberDto);
	}

	[HttpPut("{phoneNumberId}")]
	public async Task<IActionResult> UpdatePhoneNumber(int phoneNumberId, [FromBody] PhoneNumberRequestDTO phoneNumberRequest)
	{
		var phoneNumber = _mapper.Map<PhoneNumber>(phoneNumberRequest);
		await _phoneNumberService.UpdatePhoneNumberAsync(phoneNumberId, phoneNumber, phoneNumberRequest.ContactId);
		return NoContent();
	}

	[HttpDelete("{phoneNumberId}")]
	public async Task<IActionResult> DeletePhoneNumber(int phoneNumberId)
	{
		await _phoneNumberService.DeletePhoneNumberAsync(phoneNumberId);
		return NoContent();
	}

	[HttpPut("set-default/{contactId}/{phoneNumberId}")]
	public async Task<IActionResult> SetDefaultNumber(int contactId, int phoneNumberId)
	{
		await _phoneNumberService.SetDefaultNumberAsync(contactId, phoneNumberId);
		return NoContent();
	}
}