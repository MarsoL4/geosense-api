using GeoSense.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GeoSense.API.Infrastructure.Mappings
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder) 
        {
            builder
               .ToTable("Moto");

            builder
                .HasKey("Id");

            builder
                .Property(moto => moto.Modelo)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(moto => moto.Placa)
                .HasMaxLength(10);

            builder
                .HasIndex(moto => moto.Placa)
                .IsUnique();

            builder
                .Property(moto => moto.Chassi)
                .HasMaxLength(50);

            builder
                .HasIndex(moto => moto.Chassi)
                .IsUnique();

            builder
                .Property(moto => moto.Problema_Identificado)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasOne(moto => moto.Vaga) 
                .WithMany()
                .HasForeignKey(moto => moto.VagaId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
