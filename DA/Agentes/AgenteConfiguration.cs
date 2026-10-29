using Abstracciones.Agentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DA.Agentes;

public class AgenteConfiguration : IEntityTypeConfiguration<Agente>
{
    public void Configure(EntityTypeBuilder<Agente> builder)
    {
        builder.ToTable("Agentes");
        builder.HasKey(a => a.AgenteId);
        
        builder.Property(a => a.Nombre).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Correo).HasMaxLength(50);
        builder.Property(a => a.Telefono).HasMaxLength(8);
        builder.Property(a => a.CantidadTickets).HasMaxLength(50);
        builder.Property(a => a.Activo).HasDefaultValue(true);
        builder.Property(a => a.FechaCreacion).IsRequired();
    }
}