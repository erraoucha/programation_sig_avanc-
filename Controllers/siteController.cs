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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SiteCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                Geometry? geometry = null;
                try
                {
                    var reader = new GeoJsonReader();
                    geometry = reader.Read<Geometry>(dto.GeometrieGeoJson);
                    // Forcer en MultiPolygon
                    if (geometry is Polygon polygon)
                    {
                        geometry = new MultiPolygon(new[] { polygon });
                    }
                    else if (geometry is MultiPolygon multiPolygon)
                    {
                        geometry = multiPolygon;
                    }
                    else
                    {
                        ModelState.AddModelError("GeometrieGeoJson", "La géométrie n'est pas un polygone ou multipolygone.");
                        return View(dto);
                    }
                    var site = new Site
                    {
                        site = dto.site,
                        reference = dto.reference,
                        etat = dto.etat,
                        prefecture = dto.prefecture,
                        commune_ce = dto.commune_ce,
                        geom = (MultiPolygon)geometry
                    };
                    _context.Sites.Add(site);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("GeometrieGeoJson", "Erreur de lecture de la géométrie : " +
                    ex.Message);
                    return View(dto);
                }
            }
            return View(dto);
        }
    }
}