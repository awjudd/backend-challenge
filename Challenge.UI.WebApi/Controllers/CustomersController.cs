using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.UI.WebApi.DataTransfer;
using Challenge.UI.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.UI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    [HttpGet(Name = "GetAllCustomers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
    {
        return Ok((await _customerRepository.ListAllAsync()).ToDto());
    }

    [HttpGet("{id:guid}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponseDto>> GetCustomerById(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer == null)
        {
            _logger.LogInformation("Customer with id {Id} was not found.", id);
            return NotFound();
        }

        return Ok(customer.ToDto());
    }
}