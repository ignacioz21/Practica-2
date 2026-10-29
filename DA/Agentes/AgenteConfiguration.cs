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
        
        builder.Property(a => a.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Correo).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Telefono).HasMaxLength(20);
        builder.Property(a => a.CantidadTickets).HasDefaultValue(0);
        builder.Property(a => a.Activo).HasDefaultValue(true);
        builder.Property(a => a.FechaCreacion).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(a => a.Correo).IsUnique();
    }
}
