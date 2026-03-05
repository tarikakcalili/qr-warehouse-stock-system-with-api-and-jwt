using MegaQr.Api.Data;
using MegaQr.Api.Models;
using MegaQr.Api.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MegaQr.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StoreController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StoreController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("AddToStore")]
    public async Task<ActionResult<ApiResponseDto<string>>> AddToStore([FromBody] AddToStoreDto dto)
    {
        bool alreadyInStore = await _context.Stores
            .AnyAsync(s => s.ProductId == dto.ProductId && s.FromStoreExit == null);

        if (alreadyInStore)
        {
            return BadRequest(new ApiResponseDto<string>
            {
                Success = false,
                Message = "This Product is already in the current Section!",
                Data = null
            });
        }

        var storeEntry = new Store
        {
            ProductId = dto.ProductId,
            SectionId = dto.SectionId,
            ToStoreRegister = DateTime.Now,
            FromStoreExit = null
        };

        _context.Stores.Add(storeEntry);
        await _context.SaveChangesAsync();

        return Ok(new ApiResponseDto<string>
        {
            Success = true,
            Message = "Product is added in the Store",
            Data = null
        });
    }

    [HttpGet("GetStoreList")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<StoreDto>>>> GetStoreList()
    {
        var stores = await _context.Stores
            .Include(s => s.Product)
            .Include(s => s.Section)
            .Select(s => new StoreDto
            {
                StoreId = s.StoreId,
                ProductId = s.ProductId,
                ProductName = s.Product.ProductName,   
                SectionId = s.SectionId,
                SectionName = s.Section.SectionName,  
                ToStoreRegister = s.ToStoreRegister,
                FromStoreExit = s.FromStoreExit
            })
            .ToListAsync();

        return Ok(new ApiResponseDto<IEnumerable<StoreDto>>
        {
            Success = true,
            Message = "Store List.",
            Data = stores
        });
    }

    [HttpPost("RemoveFromStore")]
    public async Task<ActionResult<ApiResponseDto<Store>>> RemoveFromStore([FromBody] RemoveFromStoreDto dto)
    {
        var storeEntry = await _context.Stores
            .FirstOrDefaultAsync(s => s.ProductId == dto.ProductId && s.FromStoreExit == null);

        if (storeEntry == null)
        {
            return NotFound(new ApiResponseDto<string>
            {
                Success = false,
                Message = "This Product is not registered in the Store!",
                Data = null
            });
        }

        storeEntry.FromStoreExit = DateTime.Now;
        await _context.SaveChangesAsync();

        return Ok(new ApiResponseDto<Store>
        {
            Success = true,
            Message = "Product is out.",
            Data = null
        });
    }
}

