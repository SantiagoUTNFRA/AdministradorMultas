using AdministradorMultas.Data;
using AdministradorMultas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AdministradorMultas.Controllers
{
    public class CamaraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamaraController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ImportarCamaras(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se proporcionó ningún archivo para importar.");
            }

            List<ListaCamaras> camaras;

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var json = await stream.ReadToEndAsync();
                camaras = JsonSerializer.Deserialize<List<ListaCamaras>>(json);
            }

            foreach (var camara in camaras)
            {
                // Comprobar si la cámara ya existe en la base de datos
                var existingCamara = await _context.Camaras.FirstOrDefaultAsync(c => c.IdInspector == camara.IdInspector);  // Asumiendo que 'id' en tu JSON se mapea a 'IdInspector' en tu modelo de Camara.

                if (existingCamara != null)
                {
                    // Si la cámara existe, actualiza sus datos
                    existingCamara.Ubicacion = camara.Ubicacion; // Asumiendo que 'ubicacion' en tu JSON se mapea a 'Ubicacion' en tu modelo de Camara.
                                                                 // Aquí puedes actualizar cualquier otro campo que sea relevante
                }
                else
                {
                    // Asegúrate de que el Municipio existe
                    var muniId = camara.MunicipioId + 1;  // Incrementa el MunicipioId en 1
                    var municipio = _context.Municipios.FirstOrDefault(m => m.Id == muniId);
                    if (municipio == null)
                    {
                        // Aquí podrías lanzar un error, crear el municipio, o asignar un valor por defecto.
                        throw new Exception($"MunicipioId {muniId} no existe en la tabla Municipios");
                    }

                    // Si el municipio existe, entonces puedes crear tu nueva Camara
                    var newCamara = new ListaCamaras
                    {
                        IdInspector = camara.IdInspector,
                        IdVialControl = "0",
                        Ubicacion = camara.Ubicacion,
                        FechaAlta = DateTime.Now,
                        MunicipioId = muniId,
                        Municipios = municipio,
                        LastModification = "0"
                    };

                    // Agrega la nueva cámara a la base de datos
                    _context.Camaras.Add(newCamara);
                }
            }

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Asume que todos las cámaras en la lista importada pertenecen al mismo municipio
            int? municipioId = camaras.FirstOrDefault()?.MunicipioId;

            if (!municipioId.HasValue)
            {
                throw new Exception("La lista de cámaras importada no contiene ninguna cámara.");
            }

            return RedirectToAction("DetalleMunicipio", "Municipio", new { id = municipioId.Value + 1 });
        }


    }
}
