using MegaQr.Api.Data;
using MegaQr.Api.Models;
using MegaQr.Api.Models.DTOs;
using MegaQr.Api.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MegaQr.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductDto>>>> GetProductList()
    {
        var products = await _context.Products
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Type = p.Type,
                Date = p.Date
            })
            .ToListAsync();

        return Ok(new ApiResponseDto<IEnumerable<ProductDto>>
        {
            Success = true,
            Message = "Products fetched successfully",
            Data = products
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponseDto<ProductDto>>> CreateProduct([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            ProductName = dto.ProductName,
            Type = dto.Type,
            Date = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var responseDto = new ProductDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Type = product.Type,
            Date = product.Date
        };

        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, new ApiResponseDto<ProductDto>
        {
            Success = true,
            Message = "Product created successfully",
            Data = responseDto
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<ProductDto>>> GetProductById([FromRoute] int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new ApiResponseDto<ProductDto>
            {
                Success = false,
                Message = "Product not found",
                Data = null
            });
        }

        var dto = new ProductDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Type = product.Type,
            Date = product.Date
        };

        return Ok(new ApiResponseDto<ProductDto>
        {
            Success = true,
            Message = "Product fetched successfully",
            Data = dto
        });
    }

    [HttpPost("inStore")]
    public async Task<ActionResult<ApiResponseDto<ProductInStoreDto>>> GetProductInStore([FromBody] ProductIdDto request)
    {
        var product = await _context.Products
            .Include(p => p.Stores)
                .ThenInclude(s => s.Section)
            .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

        if (product == null)
        {
            return NotFound(new ApiResponseDto<ProductInStoreDto>
            {
                Success = false,
                Message = "Product not found",
                Data = null
            });
        }

        var lastStore = product.Stores?
            .OrderByDescending(s => s.ToStoreRegister)
            .FirstOrDefault();

        var productDto = new ProductInStoreDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Type = product.Type,
            Date = product.Date,
            SectionId = lastStore?.SectionId,
            SectionName = lastStore?.Section?.SectionName,
            ToStoreRegister = lastStore?.ToStoreRegister,
            FromStoreExit = lastStore?.FromStoreExit
        };

        var sectionsDto = await _context.Sections
        .Select(s => new SectionDto { SectionId = s.SectionId, SectionName = s.SectionName })
        .ToListAsync();

        var viewModel = new ScanProcessViewModel
        {
            Product = productDto,
            Sections = sectionsDto
        };

        var message = lastStore == null
            ? "Product has no store records"
            : lastStore.FromStoreExit == null
                ? "Product is currently in section"
                : "Product exists but is not currently in any section";

        return Ok(new ApiResponseDto<ScanProcessViewModel>
        {
            Success = true,
            Message = message,
            Data = viewModel
        });
    }
}
