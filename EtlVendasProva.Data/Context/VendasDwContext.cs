using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EtlVendasProva.Data.Domain.Entities.Dw;

namespace EtlVendasProva.Data.Context
{
    public partial class VendasDwContext : DbContext
    {
        public VendasDwContext(DbContextOptions<VendasDwContext> options)
               : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public virtual DbSet<DmClientes> DmClientes { get; set; } = null!;
        public virtual DbSet<DmProduto> DmProduto { get; set; } = null!;
        public virtual DbSet<DmTempo> DmTempo { get; set; } = null!;
        public virtual DbSet<DmVendedor> DmVendedor { get; set; } = null!;
        public virtual DbSet<FtVendas> FtVendas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DmClientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("dm_clientes_pkey");

                entity.ToTable("dm_clientes", "dimensional");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("id_cliente");

                entity.Property(e => e.ClasseCliente)
                    .HasMaxLength(10)
                    .HasColumnName("classe_cliente");

                entity.Property(e => e.EstadoCliente)
                    .HasMaxLength(2)
                    .HasColumnName("estado_cliente");

                entity.Property(e => e.NomeCliente)
                    .HasMaxLength(50)
                    .HasColumnName("nome_cliente");

                entity.Property(e => e.SexoCliente)
                    .HasMaxLength(1)
                    .HasColumnName("sexo_cliente");
            });

            modelBuilder.Entity<DmProduto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("dm_produto_pkey");

                entity.ToTable("dm_produto", "dimensional");

                entity.Property(e => e.IdProduto)
                    .ValueGeneratedNever()
                    .HasColumnName("id_produto");

                entity.Property(e => e.ClasseProduto)
                    .HasMaxLength(20)
                    .HasColumnName("classe_produto");

                entity.Property(e => e.NomeProduto)
                    .HasMaxLength(100)
                    .HasColumnName("nome_produto");

                entity.Property(e => e.PrecoProduto)
                    .HasPrecision(10, 2)
                    .HasColumnName("preco_produto");
            });

            modelBuilder.Entity<DmTempo>(entity =>
            {
                entity.HasKey(e => e.IdTempo)
                    .HasName("dm_tempo_pkey");

                entity.ToTable("dm_tempo", "dimensional");

                entity.Property(e => e.IdTempo)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tempo");

                entity.Property(e => e.DataVenda).HasColumnName("data_venda");

                entity.Property(e => e.NmMes)
                    .HasMaxLength(15)
                    .HasColumnName("nm_mes");

                entity.Property(e => e.NuMes).HasColumnName("nu_mes");

                entity.Property(e => e.SgMes)
                    .HasMaxLength(3)
                    .HasColumnName("sg_mes");

                entity.Property(e => e.Trimestre)
                    .HasMaxLength(10)
                    .HasColumnName("trimestre");
            });

            modelBuilder.Entity<DmVendedor>(entity =>
            {
                entity.HasKey(e => e.IdVendedor)
                    .HasName("dm_vendedor_pkey");

                entity.ToTable("dm_vendedor", "dimensional");

                entity.Property(e => e.IdVendedor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_vendedor");

                entity.Property(e => e.NivelVendedor)
                    .HasMaxLength(10)
                    .HasColumnName("nivel_vendedor");

                entity.Property(e => e.NomeVendedor)
                    .HasMaxLength(50)
                    .HasColumnName("nome_vendedor");
            });

            modelBuilder.Entity<FtVendas>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.IdVendedor, e.IdTempo, e.IdProduto })
                    .HasName("ft_vendas_pkey");

                entity.ToTable("ft_vendas", "dimensional");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.IdTempo).HasColumnName("id_tempo");

                entity.Property(e => e.IdProduto).HasColumnName("id_produto");

                entity.Property(e => e.DescontoTotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("desconto_total");

                entity.Property(e => e.QtdVendasRealizadas).HasColumnName("qtd_vendas_realizadas");

                entity.Property(e => e.ValTotalVenda)
                    .HasPrecision(10, 2)
                    .HasColumnName("val_total_venda");

                entity.Property(e => e.ValUnitarioProduto)
                    .HasPrecision(10, 2)
                    .HasColumnName("val_unitario_produto");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_cliente");

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdProduto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_produto");

                entity.HasOne(d => d.IdTempoNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdTempo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_tempo");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.FtVendas)
                    .HasForeignKey(d => d.IdVendedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_id_vendedor");
            });

            modelBuilder.HasSequence("idcliente", "dimensional");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
