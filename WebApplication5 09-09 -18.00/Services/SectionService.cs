using MegaQr.Api.Data;
using MegaQr.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaQr.Api.Services;

public class SectionService : ISectionService
{
    private readonly ApplicationDbContext _context;

    public SectionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Section> CreateSectionAsync()
    {
        var lastSection = await _context.Sections
            .OrderByDescending(s => s.SectionId)
            .FirstOrDefaultAsync();

        string newCode = "A1";

        if (lastSection != null)
        {
            string lastCode = lastSection.SectionName;
            char letter = lastCode[0];
            int number = int.Parse(lastCode.Substring(1));

            if (number < 5)
                number++;
            else
            {
                letter = (char)(letter + 1);
                number = 1;
            }

            newCode = $"{letter}{number}";
        }

        var newSection = new Section { SectionName = newCode };

        _context.Sections.Add(newSection);
        await _context.SaveChangesAsync();

        return newSection;
    }
}
