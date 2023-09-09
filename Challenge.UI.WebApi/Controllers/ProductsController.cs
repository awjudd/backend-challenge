using Challenge.Core.DataAccess.Contracts.Persistence;
using Challenge.UI.WebApi.DataTransfer;
using Challenge.UI.WebApi.Mappers;
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
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        return Ok((await _productRepository.ListAllAsync()).ToDto());
    }

    [HttpGet("{id:guid}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponseDto>> GetCustomerById([FromRoute] Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogInformation($"Product with id {id} was not found.", id);
            return NotFound();
        }

        return Ok(product.ToDto());
    }
}