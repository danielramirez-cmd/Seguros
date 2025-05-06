using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SegurosAPI.Models
{
    public class PolizaModel
    {

        public int Id { get; set; }

        public CatalogoModel Catalogo { get; set; }
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }   
        public int Edad { get; set;}
        public string PaisNacimiento { get; set; }
        public string Genero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }  
        public decimal MontoPrima { get; set; }
        public string Estatus { get; set; }

    } // fin clase 
} // fin namespace 
