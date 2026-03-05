using MegaQr.Api.Data;
using MegaQr.Api.Models.DTOs;
using MegaQr.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MegaQr.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _sectionService;
    private readonly ApplicationDbContext _context;

    public SectionsController(ISectionService sectionService, ApplicationDbContext context)
    {
        _sectionService = sectionService;
        _context = context;
    }

    [HttpPost("CreateSection")]
    public async Task<ActionResult<ApiResponseDto<SectionDto>>> CreateSection()
    {
        var newSection = await _sectionService.CreateSectionAsync();

        var sectionDto = new SectionDto
        {
            SectionId = newSection.SectionId,
            SectionName = newSection.SectionName
        };

        return Ok(new ApiResponseDto<SectionDto>
        {
            Success = true,
            Message = "Section created successfully",
            Data = sectionDto
        });
    }

    [HttpGet("GetSectionList")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<SectionDto>>>> GetSectionList()
    {
        var sections = await _context.Sections
            .OrderBy(s => s.SectionId)
            .Select(s => new SectionDto
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName
            })
            .ToListAsync();

        return Ok(new ApiResponseDto<IEnumerable<SectionDto>>
        {
            Success = true,
            Message = "Sections fetched successfully",
            Data = sections
        });
    }
}
