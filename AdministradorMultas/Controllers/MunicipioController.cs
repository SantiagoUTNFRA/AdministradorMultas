using AdministradorMultas.Data;
using AdministradorMultas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministradorMultas.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MunicipioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetMunicipios()
        {
            var municipios = _context.Municipios.ToList();
            return View(municipios);
        }

        public IActionResult CrearMunicipio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearMunicipio(ListaMunicipio municipio)
        {
            if (ModelState.IsValid)
            {
                _context.Municipios.Add(municipio);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetMunicipios");
            }
            return View(municipio);
        }

        public async Task<IActionResult> DetalleMunicipio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios
                .Include(m => m.Camaras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }


    }
}