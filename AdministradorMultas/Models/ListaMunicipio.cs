namespace AdministradorMultas.Models
{
    public class ListaMunicipio
    {
        public ListaMunicipio()
        {
            Camaras = new List<ListaCamaras>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ListaCamaras> Camaras { get; set; }
    }
}
