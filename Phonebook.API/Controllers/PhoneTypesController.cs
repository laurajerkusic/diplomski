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
public class PhoneTypesController : ControllerBase
{
	private readonly IPhoneTypeService _phoneTypeService;
	private readonly IMapper _mapper;

	public PhoneTypesController(IPhoneTypeService phoneTypeService, IMapper mapper)
	{
		_phoneTypeService = phoneTypeService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<PhoneTypeDTO>>> GetAllPhoneTypes()
	{
		var phoneTypes = await _phoneTypeService.GetAllPhoneTypesAsync();
		return Ok(_mapper.Map<IEnumerable<PhoneTypeDTO>>(phoneTypes));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PhoneTypeDTO>> GetPhoneType(int id)
	{
		var phoneType = await _phoneTypeService.GetPhoneTypeByIdAsync(id);
		return Ok(_mapper.Map<PhoneTypeDTO>(phoneType));
	}

	[HttpPost]
	public async Task<ActionResult<PhoneTypeDTO>> AddPhoneType([FromBody] PhoneTypeRequestDTO phoneTypeRequest)
	{
		var phoneType = _mapper.Map<PhoneType>(phoneTypeRequest);
		var createdPhoneType = await _phoneTypeService.AddPhoneTypeAsync(phoneType);
		var phoneTypeDto = _mapper.Map<PhoneTypeDTO>(createdPhoneType);
		return CreatedAtAction(nameof(GetPhoneType), new { id = phoneTypeDto.Id }, phoneTypeDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletePhoneType(int id)
	{
		await _phoneTypeService.DeletePhoneTypeAsync(id);
		return NoContent();
	}
}