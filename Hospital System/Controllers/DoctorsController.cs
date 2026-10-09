using Hospital_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();
    private const int PageSize = 3;

    public IActionResult Index(string? name, string? specialization, int page = 1)
    {
        var doctors = _context.Doctors.AsQueryable();

        // Filter
        if (!string.IsNullOrWhiteSpace(name))
            doctors = doctors.Where(d => d.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(specialization))
            doctors = doctors.Where(d => d.Specialization == specialization);

        // Pagination
        var totalCount = doctors.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        if (page < 1) page = 1;
        if (totalPages > 0 && page > totalPages) page = totalPages;

        var pagedDoctors = doctors
            .OrderBy(d => d.Id)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        ViewBag.Name = name;
        ViewBag.Specialization = specialization;
        ViewBag.Specializations = _context.Doctors
            .Select(d => d.Specialization)
            .Distinct()
            .ToList();
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View(pagedDoctors);
    }
}