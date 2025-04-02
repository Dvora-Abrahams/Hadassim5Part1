using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<GoodsToOrder> GoodsToOrders { get; set; }

    public virtual DbSet<GoodsToSupplier> GoodsToSuppliers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Documents\\groceryDatabase.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Goods__3214EC07B40FBA6F");

            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GoodsToOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoodsToO__3214EC07DBF99BF6");

            entity.HasOne(d => d.Goods).WithMany(p => p.GoodsToOrders)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToOrders_ToTable");

            entity.HasOne(d => d.Orders).WithMany(p => p.GoodsToOrders)
                .HasForeignKey(d => d.OrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToOrders_ToTable1");
        });

        modelBuilder.Entity<GoodsToSupplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoodsToS__3214EC0740104953");

            entity.HasOne(d => d.Goods).WithMany(p => p.GoodsToSuppliers)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToSuppliers_ToTable_1");

            entity.HasOne(d => d.Suppliers).WithMany(p => p.GoodsToSuppliers)
                .HasForeignKey(d => d.SuppliersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToSuppliers_ToTable");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07595079A9");

            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("waiting");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_ToTable");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC0707927B74");

            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
