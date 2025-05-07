using SegurosAPI.Models;

namespace SegurosAPI.DTOs
{
    public class PolizaDTOCreate
    {
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public int Edad { get; set; }
        public string PaisNacimiento { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }
        public decimal MontoPrima { get; set; }
        public string Estatus { get; set; }

    } // fin clase 
} // fin namespace
