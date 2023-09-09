using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
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
    [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
    {
        return Ok(await _customerRepository.ListAllAsync());
    }

    [HttpGet("{id:guid}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }
}