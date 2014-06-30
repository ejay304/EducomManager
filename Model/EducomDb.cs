    namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EducomDb : DbContext
    {
        public EducomDb()
            : base("name=EducomDb")
        {
        }

        public virtual DbSet<request> requests { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<request>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<request>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<request>()
                .HasMany(e => e.students)
                .WithMany(e => e.requests)
                .Map(m => m.ToTable("requests_has_students", "prototype").MapLeftKey("requests_id").MapRightKey("students_id"));

            modelBuilder.Entity<student>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.users_id)
                .WillCascadeOnDelete(false);
        }
    }
}
