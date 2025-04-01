using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsuranceProviderManagementSystem.Models;

public partial class InsuranceProviderManagementSystemContext : DbContext
{
    public InsuranceProviderManagementSystemContext()
    {
    }

    public InsuranceProviderManagementSystemContext(DbContextOptions<InsuranceProviderManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }
    public virtual DbSet<UserSessionLog> UserSessionLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JQ4K9V9\\SQLEXPRESS;Database=InsuranceProviderManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B752EF37E");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProviderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProviderID");

            entity.HasOne(d => d.Provider).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK__Category__Provid__5AEE82B9");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21A9611F58F24");

            entity.ToTable("City");

            entity.Property(e => e.CityId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__City__StateID__52593CB8");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claim__EF2E13BBEBB0FCF4");

            entity.ToTable("Claim");

            entity.Property(e => e.ClaimId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ClaimID");
            entity.Property(e => e.ClaimAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ClaimReason)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClaimStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CustomerID");
            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PolicyID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Claims)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Claim__CustomerI__6E01572D");

            entity.HasOne(d => d.Policy).WithMany(p => p.Claims)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__Claim__PolicyID__6D0D32F4");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8675C1B36");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CityId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CityID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EmailID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PolicyID");

            entity.HasOne(d => d.City).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Customer__CityID__6A30C649");

            entity.HasOne(d => d.Policy).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__Customer__Policy__693CA210");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A583739A112");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PaymentID");
            entity.Property(e => e.Amount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CustomerID");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PolicyID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Payment__Custome__71D1E811");

            entity.HasOne(d => d.Policy).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__Payment__PolicyI__72C60C4A");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policy__2E133944EE20CAB3");

            entity.ToTable("Policy");

            entity.Property(e => e.PolicyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PolicyID");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PremiumAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubCategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SubCategoryID");
            entity.Property(e => e.SumAssured).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Policies)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__Policy__SubCateg__66603565");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__Provider__B54C689D695C08DB");

            entity.ToTable("Provider");

            entity.Property(e => e.ProviderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProviderID");
            entity.Property(e => e.CityId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CityID");
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EmailID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ProviderName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Providers)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Provider__CityID__5812160E");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__State__C3BA3B5ADC8C36A5");

            entity.ToTable("State");

            entity.Property(e => e.StateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StateID");
            entity.Property(e => e.CapitalName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StateCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("PK__SubCateg__26BE5BF9180671EE");

            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SubCategoryID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__SubCatego__Categ__5DCAEF64");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACE664605C");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserDeta__1788CCAC945429DD");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<UserSessionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSess__3214EC07ADF34A48");
            entity.Property(e => e.LoginTime).HasColumnType("datetime");
            entity.Property(e => e.LogoutTime).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
