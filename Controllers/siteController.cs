using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBGIS.Data; // Adjust namespace for your DataContext
using WEBGIS.Models; // Adjust namespace for your commune model
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
namespace WEBGIS.Controllers
{
    public class SiteController : Controller
    {
        private readonly AppDbContext _context;
        public SiteController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Sites.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}