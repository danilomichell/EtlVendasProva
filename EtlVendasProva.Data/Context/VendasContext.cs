using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EtlVendasProva.Data.Domain.Entities.Relacional;

namespace EtlVendasProva.Data.Context
{
    public partial class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        public virtual DbSet<Clientes> Clientes { get; set; } = null!;
        public virtual DbSet<Itensvenda> Itensvenda { get; set; } = null!;
        public virtual DbSet<Produtos> Produtos { get; set; } = null!;
        public virtual DbSet<Vendas> Vendas { get; set; } = null!;
        public virtual DbSet<Vendedores> Vendedores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.Idcliente)
                    .HasName("clientes_pkey");

                entity.ToTable("clientes", "relacional");

                entity.Property(e => e.Idcliente)
                    .HasColumnName("idcliente")
                    .HasDefaultValueSql("nextval('relacional.idcliente'::regclass)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(50)
                    .HasColumnName("cliente");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .HasColumnName("estado");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .HasColumnName("sexo");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Itensvenda>(entity =>
            {
                entity.HasKey(e => new { e.Idproduto, e.Idvenda })
                    .HasName("itensvenda_pkey");

                entity.ToTable("itensvenda", "relacional");

                entity.Property(e => e.Idproduto).HasColumnName("idproduto");

                entity.Property(e => e.Idvenda).HasColumnName("idvenda");

                entity.Property(e => e.Desconto)
                    .HasPrecision(10, 2)
                    .HasColumnName("desconto");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Valortotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("valortotal");

                entity.Property(e => e.Valorunitario)
                    .HasPrecision(10, 2)
                    .HasColumnName("valorunitario");

                entity.HasOne(d => d.IdprodutoNavigation)
                    .WithMany(p => p.Itensvenda)
                    .HasForeignKey(d => d.Idproduto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("itensvenda_idproduto_fkey");

                entity.HasOne(d => d.IdvendaNavigation)
                    .WithMany(p => p.Itensvenda)
                    .HasForeignKey(d => d.Idvenda)
                    .HasConstraintName("itensvenda_idvenda_fkey");
            });

            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.HasKey(e => e.Idproduto)
                    .HasName("produtos_pkey");

                entity.ToTable("produtos", "relacional");

                entity.Property(e => e.Idproduto)
                    .HasColumnName("idproduto")
                    .HasDefaultValueSql("nextval('relacional.idproduto'::regclass)");

                entity.Property(e => e.Preco)
                    .HasPrecision(10, 2)
                    .HasColumnName("preco");

                entity.Property(e => e.Produto)
                    .HasMaxLength(100)
                    .HasColumnName("produto");
            });

            modelBuilder.Entity<Vendas>(entity =>
            {
                entity.HasKey(e => e.Idvenda)
                    .HasName("vendas_pkey");

                entity.ToTable("vendas", "relacional");

                entity.Property(e => e.Idvenda)
                    .HasColumnName("idvenda")
                    .HasDefaultValueSql("nextval('relacional.idvenda'::regclass)");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Idcliente).HasColumnName("idcliente");

                entity.Property(e => e.Idvendedor).HasColumnName("idvendedor");

                entity.Property(e => e.Total)
                    .HasPrecision(10, 2)
                    .HasColumnName("total");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.Vendas)
                    .HasForeignKey(d => d.Idcliente)
                    .HasConstraintName("vendas_idcliente_fkey");

                entity.HasOne(d => d.IdvendedorNavigation)
                    .WithMany(p => p.Vendas)
                    .HasForeignKey(d => d.Idvendedor)
                    .HasConstraintName("vendas_idvendedor_fkey");
            });

            modelBuilder.Entity<Vendedores>(entity =>
            {
                entity.HasKey(e => e.Idvendedor)
                    .HasName("vendedores_pkey");

                entity.ToTable("vendedores", "relacional");

                entity.Property(e => e.Idvendedor)
                    .HasColumnName("idvendedor")
                    .HasDefaultValueSql("nextval('relacional.idvendedor'::regclass)");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .HasColumnName("nome");
            });

            modelBuilder.HasSequence("idcliente", "relacional");

            modelBuilder.HasSequence("idproduto", "relacional");

            modelBuilder.HasSequence("idvenda", "relacional");

            modelBuilder.HasSequence("idvendedor", "relacional");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
