using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Models;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyInfo> CompanyInfos { get; set; }

    public virtual DbSet<ProductInfo> ProductInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MFEN465;database=ProductDb;trusted_connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyInfo>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__CompanyI__C1F8DC391D5B0281");

            entity.ToTable("CompanyInfo");

            entity.HasIndex(e => e.Cname, "UQ__CompanyI__85D445AA77608107").IsUnique();

            entity.Property(e => e.Cid)
                .ValueGeneratedNever()
                .HasColumnName("CId");
            entity.Property(e => e.Cname)
                .HasMaxLength(10)
                .HasColumnName("CName");
        });

        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__ProductI__C577554092B29718");

            entity.ToTable("ProductInfo");

            entity.Property(e => e.Pid)
                .ValueGeneratedNever()
                .HasColumnName("PId");
            entity.Property(e => e.Cid).HasColumnName("CId");
            entity.Property(e => e.Pmdate)
                .HasColumnType("date")
                .HasColumnName("PMdate");
            entity.Property(e => e.Pname)
                .HasMaxLength(50)
                .HasColumnName("PName");
            entity.Property(e => e.Pprice).HasColumnName("PPrice");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.ProductInfos)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK__ProductInfo__CId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
