namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.programs")]
    public partial class program
    {
        private DateTime? _begin_date { get; set; }
        private DateTime? _end_date { get; set; }
        public program()
        {
            contacts = new HashSet<contact>();
            invoices = new HashSet<invoice>();
            propositions = new HashSet<proposition>();
            campus = new HashSet<campu>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? begin_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? end_date { get; set; }

        public int program_types_id { get; set; }

        public int organisation_id { get; set; }

        public bool active { get; set; }

        public virtual ICollection<contact> contacts { get; set; }

        public virtual ICollection<invoice> invoices { get; set; }

        public virtual organisation organisation { get; set; }

        public virtual program_types program_types { get; set; }

        public virtual ICollection<proposition> propositions { get; set; }

        public virtual ICollection<campu> campus { get; set; }
    }
}
