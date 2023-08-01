using System.ComponentModel.DataAnnotations;
using AdministradorMultas.Models;


namespace AdministradorMultas.Models
{
    public class ListaCamaras
    {
        [Key]
        public string IdInspector { get; set; }
        public string IdVialControl { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public string LastModification { get; set; }
        public int MunicipioId { get; set; }
        public ListaMunicipio Municipios { get; set; }
    }
}
