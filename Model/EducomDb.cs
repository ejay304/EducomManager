using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity.Migrations;

namespace PrototypeEDUCOM.Model
{

    /// <filename>EducomDb.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe contenant tous les DbSet des modèles, avec la définition des relations entres 
    ///     les classes. Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    public partial class EducomDb : DbContext
    {

        private static EducomDb instance;

        public EducomDb() : base("name=EducomDb")
        {

        }

        public static EducomDb getInstance()
        {
            if (instance == null)
            {
                instance = new EducomDb();
                instance.Database.Log = Console.WriteLine;
            }
            return instance;
        }

        public virtual DbSet<Campaign> campaigns { get; set; }
        public virtual DbSet<Campus> campus { get; set; }
        public virtual DbSet<Contact> contacts { get; set; }
        public virtual DbSet<Email> emails { get; set; }
        public virtual DbSet<EventType> event_types { get; set; }
        public virtual DbSet<Event> events { get; set; }
        public virtual DbSet<Invoice> invoices { get; set; }
        public virtual DbSet<Organisation> organisations { get; set; }
        public virtual DbSet<Phone> phones { get; set; }
        public virtual DbSet<ProgramType> program_types { get; set; }
        public virtual DbSet<Program> programs { get; set; }
        public virtual DbSet<Proposition> propositions { get; set; }
        public virtual DbSet<Question> questions { get; set; }
        public virtual DbSet<Request> requests { get; set; }
        public virtual DbSet<Respons> responses { get; set; }
        public virtual DbSet<Student> students { get; set; }
        public virtual DbSet<Survey> surveys { get; set; }
        public virtual DbSet<User> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign>()
                .Property(e => e.state)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Campaign>()
                .HasMany(e => e.contacts)
                .WithMany(e => e.campaigns)
                .Map(m => m.ToTable("distributions", "EducomDb").MapLeftKey("campaigns_id").MapRightKey("contacts_id"));

            modelBuilder.Entity<Campus>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Campus>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<Campus>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Campus>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<Campus>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<Campus>()
                .HasMany(e => e.propositions)
                .WithRequired(e => e.campu)
                .HasForeignKey(e => e.campus_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Campus>()
                .HasMany(e => e.programs)
                .WithMany(e => e.campus)
                .Map(m => m.ToTable("locations", "EducomDb").MapLeftKey("campus_id").MapRightKey("programs_id"));

            modelBuilder.Entity<Contact>()
                .Property(e => e.civility)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.district)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.situation)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.promo_type)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.contact_type)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.particule)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.emails)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.phones)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.students)
                .WithRequired(e => e.contact)
                .HasForeignKey(e => e.contacts_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.email1)
                .IsUnicode(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Email>()
                .HasMany(e => e.organisations)
                .WithRequired(e => e.email)
                .HasForeignKey(e => e.emails_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventType>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<EventType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.events)
                .WithRequired(e => e.event_types)
                .HasForeignKey(e => e.event_types_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.foreign_type)
                .IsUnicode(false);

            modelBuilder.Entity<Organisation>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Organisation>()
                .Property(e => e.street)
                .IsUnicode(false);

            modelBuilder.Entity<Organisation>()
                .Property(e => e.city)
                .IsUnicode(false);

            modelBuilder.Entity<Organisation>()
                .Property(e => e.zip)
                .IsUnicode(false);

            modelBuilder.Entity<Organisation>()
                .Property(e => e.country)
                .IsUnicode(false);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.contacts)
                .WithOptional(e => e.organisation)
                .HasForeignKey(e => e.organisation_id);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.programs)
                .WithRequired(e => e.organisation)
                .HasForeignKey(e => e.organisation_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phone>()
                .Property(e => e.number)
                .IsUnicode(false);

            modelBuilder.Entity<Phone>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Phone>()
                .HasMany(e => e.organisations)
                .WithRequired(e => e.phone)
                .HasForeignKey(e => e.phones_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramType>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramType>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramType>()
                .HasMany(e => e.programs)
                .WithRequired(e => e.program_types)
                .HasForeignKey(e => e.program_types_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.contacts)
                .WithOptional(e => e.program)
                .HasForeignKey(e => e.programs_id);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.invoices)
                .WithRequired(e => e.program)
                .HasForeignKey(e => e.programs_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.propositions)
                .WithRequired(e => e.program)
                .HasForeignKey(e => e.programs_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.question1)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.choice)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.responses)
                .WithRequired(e => e.question)
                .HasForeignKey(e => e.questions_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.surveys)
                .WithMany(e => e.questions)
                .Map(m => m.ToTable("survey_contents", "EducomDb").MapLeftKey("questions_id").MapRightKey("surveys_id"));

            modelBuilder.Entity<Request>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.journey_type)
                .IsUnicode(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.events)
                .WithRequired(e => e.request)
                .HasForeignKey(e => e.requests_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.propositions)
                .WithRequired(e => e.request)
                .HasForeignKey(e => e.requests_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.responses)
                .WithRequired(e => e.request)
                .HasForeignKey(e => e.requests_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Respons>()
                .Property(e => e.response)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.kinship)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.students_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Survey>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.salt)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.requests)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.users_id)
                .WillCascadeOnDelete(false);
        }
    }
}
