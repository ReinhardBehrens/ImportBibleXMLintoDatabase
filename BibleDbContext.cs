using ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportBibleXMLintoDatabase
{
    public partial class BibleDbContext : DbContext
    {
        public BibleDbContext()
        {
        }

        public BibleDbContext(DbContextOptions<BibleDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BibleBook> BibleBooks { get; set; }

        public virtual DbSet<BibleChapter> BibleChapters { get; set; }

        public virtual DbSet<BibleVerse> BibleVerses { get; set; }

        public virtual DbSet<BibleVersion> BibleVersions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BibleForAllServerAppv1.Server.Data;Trusted_Connection=False;Integrated Security=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BibleBook>(entity =>
            {
                entity.ToTable("BibleBook");
            });

            modelBuilder.Entity<BibleChapter>(entity =>
            {
                entity.ToTable("BibleChapter");

                entity.HasIndex(e => e.BibleBookId, "IX_BibleChapter_BibleBookId");

                entity.HasIndex(e => e.BibleVersionId, "IX_BibleChapter_BibleVersionId");

                entity.HasOne(d => d.BibleBook).WithMany(p => p.BibleChapters).HasForeignKey(d => d.BibleBookId);

                entity.HasOne(d => d.BibleVersion).WithMany(p => p.BibleChapters).HasForeignKey(d => d.BibleVersionId);
            });

            modelBuilder.Entity<BibleVerse>(entity =>
            {
                entity.HasIndex(e => e.BibleChapterId, "IX_BibleVerses_BibleChapterId");

                entity.HasIndex(e => e.BibleVersionId, "IX_BibleVerses_BibleVersionId");

                entity.HasOne(d => d.BibleChapter).WithMany(p => p.BibleVerses).HasForeignKey(d => d.BibleChapterId);

                entity.HasOne(d => d.BibleVersion).WithMany(p => p.BibleVerses).HasForeignKey(d => d.BibleVersionId);
            });

            modelBuilder.Entity<BibleVersion>(entity =>
            {
                entity.ToTable("BibleVersion");

                entity.Property(e => e.HistorySummary).HasColumnName("History_summary");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
