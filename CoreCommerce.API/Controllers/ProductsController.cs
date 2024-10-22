using AutoMapper;
using CoreCommerce.API.Dtos;
using CoreCommerce.API.Extensions;
using CoreCommerce.API.Helper;
using CoreCommerce.Core.Entities;
using CoreCommerce.Core.Interfaces;
using CoreCommerce.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CoreCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> _productsRepo ,IGenericRepository<Category> _categoryRepo, IMapper _mapper) : ControllerBase
{
    [HttpGet("GetProducts")]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductParams productParams)
    {
        var spec = new ProductsSpecification(productParams);
        var countSpec = new ProductsSpecification(productParams);

        var totalItems = await _productsRepo.CountAsync(countSpec);
        var products = await _productsRepo.ListAsync(spec);

        var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

        return Ok(new Pagination<ProductDto>(productParams.PageIndex,productParams.PageSize, totalItems, data));
    }

    [HttpGet("GetProduct/{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        if (id <= 0)
        {
            return NotFound(new ApiResponse<string>(404, "No Data", "NotFound"));
        }
        var spec = new ProductsSpecification(id);

        var product = await _productsRepo.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse<object>(404 , "No Data" , "Not Found"));

        var productDto =  _mapper.Map<ProductDto>(product);
        return Ok(new ApiResponse<ProductDto>(200 , productDto, "Returned Successfully"));
    }

    [HttpGet("GetProductCategories")]
    public async Task<ActionResult<IReadOnlyList<Category>>> GetProductCategories()
    {
        return Ok(await _categoryRepo.ListAllAsync());
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult> AddProduct([FromForm] ProductCreateDto productCreateDto)
    {
        if (productCreateDto is null)
        {
            return NotFound(new ApiResponse<string>(404, "No Data", "Not Found"));
        }
        string imageUrl = string.Empty;
        if (productCreateDto.Img != null)
        {
            imageUrl = await productCreateDto.Img.UploadFile("product-images");
        }
        var product = _mapper.Map<Product>(productCreateDto);

        product.ImgUrl = imageUrl;

        _productsRepo.Add(product);
        var result = await _productsRepo.SaveAllAsync();

        if (result)
        {
            return Ok(new ApiResponse<Product>(200, product ,"Product created successfully."));
        }

        return BadRequest(new ApiResponse<string>(400,"No Data", "Failed to create the product."));
    }

}