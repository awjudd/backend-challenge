using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.UI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [HttpGet(Name = "GetAllProducts")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        return Ok(await _productRepository.ListAllAsync());
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}