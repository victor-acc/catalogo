using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace catalogo.Models;

public partial class AddventuraContext : DbContext
{
    public AddventuraContext()
    {
    }

    public AddventuraContext(DbContextOptions<AddventuraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=VICTORPC\\SQLEXPRESS;Initial Catalog=addventura;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Idcliente).HasName("PK__Clientes__95BA769AA1535848");

            entity.Property(e => e.Idcliente).HasColumnName("IDcliente");
            entity.Property(e => e.Correo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedidos__3214EC27542AE1BC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedidos__cantida__4D94879B");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedidos__idprodu__4E88ABD4");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__A543D3ED82078528");

            entity.Property(e => e.Idproducto).HasColumnName("IDproducto");
            entity.Property(e => e.Nombreproducto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
