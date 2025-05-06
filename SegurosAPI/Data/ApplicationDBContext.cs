using Microsoft.EntityFrameworkCore;
using SegurosAPI.DTOs;
using SegurosAPI.Models;

namespace SegurosAPI.Data
{
    public class ApplicationDBContext: DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aquí agregas la configuración del DTO
            modelBuilder.Entity<CatalogoDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<PolizaDTO>().HasNoKey().ToView(null);
        }


        DbSet<CatalogoModel> CatalogoPoliza { get; set; }
        DbSet<PolizaModel> Poliza { get; set; }

    } // fin clase 
} // fin namespace
