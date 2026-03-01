using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using ShoeShop.Entities;

namespace ShoeShop.DbContexts;

public partial class ShoeshopContext : DbContext
{
    public ShoeshopContext()
    {
    }

    public ShoeshopContext(DbContextOptions<ShoeshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacrurer> Manufacrurers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Prodname> Prodnames { get; set; }

    public virtual DbSet<Prodorder> Prodorders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Pvz> Pvzs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usersorder> Usersorders { get; set; }

    public virtual DbSet<Usersrole> Usersroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=shoeshop;user=root;password=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCat).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.IdCat).HasColumnName("idCat");
            entity.Property(e => e.CatName).HasMaxLength(255);
        });

        modelBuilder.Entity<Manufacrurer>(entity =>
        {
            entity.HasKey(e => e.IdManuf).HasName("PRIMARY");

            entity.ToTable("manufacrurers");

            entity.Property(e => e.IdManuf).HasColumnName("idManuf");
            entity.Property(e => e.ManufName).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.PvzId, "PvzId");

            entity.HasIndex(e => e.StatusId, "StatusId");

            entity.Property(e => e.IdOrder).HasColumnName("idOrder");

            entity.HasOne(d => d.Pvz).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PvzId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_ibfk_1");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orders_ibfk_2");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("orderstatus");

            entity.Property(e => e.IdStatus).HasColumnName("idStatus");
            entity.Property(e => e.StatusName).HasMaxLength(255);
        });

        modelBuilder.Entity<Prodname>(entity =>
        {
            entity.HasKey(e => e.IdProdName).HasName("PRIMARY");

            entity.ToTable("prodnames");

            entity.Property(e => e.IdProdName).HasColumnName("idProdName");
            entity.Property(e => e.ProdName1)
                .HasMaxLength(255)
                .HasColumnName("ProdName");
        });

        modelBuilder.Entity<Prodorder>(entity =>
        {
            entity.HasKey(e => e.IdProdOrder).HasName("PRIMARY");

            entity.ToTable("prodorders");

            entity.HasIndex(e => e.OrderId, "OrderId");

            entity.HasIndex(e => e.ProdId, "ProdId");

            entity.Property(e => e.IdProdOrder).HasColumnName("idProdOrder");

            entity.HasOne(d => d.Order).WithMany(p => p.Prodorders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prodorders_ibfk_2");

            entity.HasOne(d => d.Prod).WithMany(p => p.Prodorders)
                .HasForeignKey(d => d.ProdId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("prodorders_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProd).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.CatId, "CatId");

            entity.HasIndex(e => e.ManufId, "ManufId");

            entity.HasIndex(e => e.ProdName, "ProdName");

            entity.HasIndex(e => e.SupId, "SupId");

            entity.Property(e => e.IdProd).HasColumnName("idProd");
            entity.Property(e => e.Article).HasMaxLength(255);
            entity.Property(e => e.Descrip).HasColumnType("text");
            entity.Property(e => e.Image).HasColumnType("text");
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.Cat).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("products_ibfk_4");

            entity.HasOne(d => d.Manuf).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufId)
                .HasConstraintName("products_ibfk_3");

            entity.HasOne(d => d.ProdNameNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdName)
                .HasConstraintName("products_ibfk_2");

            entity.HasOne(d => d.Sup).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupId)
                .HasConstraintName("products_ibfk_1");
        });

        modelBuilder.Entity<Pvz>(entity =>
        {
            entity.HasKey(e => e.IdPvz).HasName("PRIMARY");

            entity.ToTable("pvz");

            entity.Property(e => e.IdPvz).HasColumnName("idPvz");
            entity.Property(e => e.Adress).HasMaxLength(255);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.IdSup).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.Property(e => e.IdSup).HasColumnName("idSup");
            entity.Property(e => e.SupName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.RoleId, "RoleId");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.Passw).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<Usersorder>(entity =>
        {
            entity.HasKey(e => e.IdUserOrder).HasName("PRIMARY");

            entity.ToTable("usersorders");

            entity.HasIndex(e => e.OrderId, "OrderId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.IdUserOrder).HasColumnName("idUserOrder");

            entity.HasOne(d => d.Order).WithMany(p => p.Usersorders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usersorders_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Usersorders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usersorders_ibfk_1");
        });

        modelBuilder.Entity<Usersrole>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("usersroles");

            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.RoleName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
