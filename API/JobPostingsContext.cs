using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public partial class JobPostingsContext : DbContext
    {
        public JobPostingsContext()
        {
        }

        public JobPostingsContext(DbContextOptions<JobPostingsContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyContact> CompanyContact { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobCompanies> JobCompanies { get; set; }
        public virtual DbSet<JobCategory> JobCategory { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Server=PCRALFS\\SQLEXPRESS;Database=JobsPosting;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CompanyContact>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK__CompanyCo__70DAFC34A95F26A8");

                entity.Property(e => e.CompanyId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.ContactNumber).HasMaxLength(15);

                entity.HasOne(d => d.Company)
                    .WithOne(p => p.CompanyContact)
                    .HasForeignKey<CompanyContact>(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CompanyCon__Autho__398D8EEE");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Job__CategoryId__403A8C7D");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Job__PublisherI__412EB0B6");
            });

            modelBuilder.Entity<JobCompanies>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.CompanyId })
                    .HasName("PK__JobAuth__6AED6DC4F50D1BAA");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.JobCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JobAutho__Autho__44FF419A");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobCompanies)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JobAutho__JobI__440B1D61");
            });

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}