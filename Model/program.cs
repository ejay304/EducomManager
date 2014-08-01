using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Program.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux programmes des organisations.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.programs")]
    public partial class Program
    {
        public Program()
        {
            contacts = new HashSet<Contact>();
            invoices = new HashSet<Invoice>();
            propositions = new HashSet<Proposition>();
            campus = new HashSet<Campus>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? begin_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? end_date { get; set; }

        public int program_types_id { get; set; }

        public int organisation_id { get; set; }

        public bool active { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        public virtual ICollection<Contact> contacts { get; set; }

        public virtual ICollection<Invoice> invoices { get; set; }

        public virtual Organisation organisation { get; set; }

        public virtual ProgramType program_types { get; set; }

        public virtual ICollection<Proposition> propositions { get; set; }

        public virtual ICollection<Campus> campus { get; set; }

        public override string ToString()
        {
            return this.organisation.name + " - " + this.program_types.name;
        }
    }
}
