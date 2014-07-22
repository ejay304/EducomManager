namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EducomDb : DbContext
    {

        private static EducomDb instance;

        public EducomDb()
            : base("name=EducomDb")
        {
        }

        public static EducomDb getInstance()
        {
            if (instance == null)
                instance = new EducomDb();
            return instance;
        }

        public virtual DbSet<campaign> campaigns { get; set; }
        public virtual DbSet<campu> campus { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<email> emails { get; set; }
        public virtual DbSet<event_types> event_types { get; set; }
        public virtual DbSet<_event> events { get; set; }
        public virtual DbSet<invoice> invoices { get; set; }
        public virtual DbSet<organisation> organisations { get; set; }
        public virtual DbSet<phone> phones { get; set; }
        public virtual DbSet<program_types> program_types { get; set; }
        public virtual DbSet<program> programs { get; set; }
        public virtual DbSet<proposition> propositions { get; set; }
        public virtual DbSet<question> questions { get; set; }
        public virtual DbSet<request> requests { get; set; }
        public virtual DbSet<respons> responses { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<survey> surveys { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<campaign>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<campaign>()
                .HasMany(e => e.contacts)
                .WithMany(e => e.campaigns)
                .Map(m => m.ToTable("distributions", "EducomDb").MapLeftKey("campaigns_id").MapRightKey("contacts_id"));

            modelBuilder.Entity<campu>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<campu>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<campu>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<campu>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<campu>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<campu>()
                .HasMany(e => e.propositions)
                .WithRequired(e => e.campu)
                .HasForeignKey(e => e.campus_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<campu>()
                .HasMany(e => e.programs)
                .WithMany(e => e.campus)
                .Map(m => m.ToTable("locations", "EducomDb").MapLeftKey("campus_id").MapRightKey("programs_id"));

            modelBuilder.Entity<contact>()
                .Property(e => e.civility)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.district)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.situation)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.promo_type)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.contact_type)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.particule)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .HasMany(e => e.emails)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<contact>()
                .HasMany(e => e.phones)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<contact>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<contact>()
                .HasMany(e => e.students)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<email>()
                .Property(e => e.email1)
                .IsUnicode(false);

            modelBuilder.Entity<email>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<email>()
                .HasMany(e => e.organisations)
                .WithRequired(e => e.email)
                .HasForeignKey(e => e.emails_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<event_types>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<event_types>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<event_types>()
                .HasMany(e => e.events)
                .WithRequired(e => e.event_types)
                .HasForeignKey(e => e.event_types_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<invoice>()
                .Property(e => e.foreign_type)
                .IsUnicode(false);

            modelBuilder.Entity<organisation>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<organisation>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<organisation>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<organisation>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<organisation>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<organisation>()
                .HasMany(e => e.contacts)
                .WithOptional(e => e.organisation)
                .HasForeignKey(e => e.organisation_id);

            modelBuilder.Entity<organisation>()
                .HasMany(e => e.programs)
                .WithRequired(e => e.organisation)
                .HasForeignKey(e => e.organisation_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<phone>()
                .Property(e => e.number)
                .IsUnicode(false);

            modelBuilder.Entity<phone>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<phone>()
                .HasMany(e => e.organisations)
                .WithRequired(e => e.phone)
                .HasForeignKey(e => e.phones_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<program_types>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<program_types>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<program_types>()
                .HasMany(e => e.programs)
                .WithRequired(e => e.program_types)
                .HasForeignKey(e => e.program_types_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<program>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<program>()
                .HasMany(e => e.contacts)
                .WithOptional(e => e.program)
                .HasForeignKey(e => e.programs_id);

            modelBuilder.Entity<program>()
                .HasMany(e => e.invoices)
                .WithRequired(e => e.program)
                .HasForeignKey(e => e.programs_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<program>()
                .HasMany(e => e.propositions)
                .WithRequired(e => e.program)
                .HasForeignKey(e => e.programs_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<question>()
                .Property(e => e.question1)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .Property(e => e.choice)
                .IsUnicode(false);

            modelBuilder.Entity<question>()
                .HasMany(e => e.responses)
                .WithRequired(e => e.question)
                .HasForeignKey(e => e.questions_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<question>()
                .HasMany(e => e.surveys)
                .WithMany(e => e.questions)
                .Map(m => m.ToTable("survey_contents", "EducomDb").MapLeftKey("questions_id").MapRightKey("surveys_id"));

            modelBuilder.Entity<request>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<request>()
                .Property(e => e.journey_type)
                .IsUnicode(false);

            modelBuilder.Entity<request>()
                .HasMany(e => e.events)
                .WithRequired(e => e.request)
                .HasForeignKey(e => e.requests_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<request>()
                .HasMany(e => e.propositions)
                .WithRequired(e => e.request)
                .HasForeignKey(e => e.requests_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<request>()
                .HasMany(e => e.responses)
                .WithRequired(e => e.request)
                .HasForeignKey(e => e.requests_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<respons>()
                .Property(e => e.response)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.kinship)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.students_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<survey>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.salt)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.users_id)
                .WillCascadeOnDelete(false);
        }
    }
}
