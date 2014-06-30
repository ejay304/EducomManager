namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.requests")]
    public partial class request
    {
        public request()
        {
            contacts = new HashSet<contact>();
            events = new HashSet<_event>();
            propositions = new HashSet<proposition>();
            responses = new HashSet<respons>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime creation_date { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string comment { get; set; }

        public int students_id { get; set; }

        public int users_id { get; set; }

        public bool active { get; set; }

        public virtual ICollection<contact> contacts { get; set; }

        public virtual ICollection<_event> events { get; set; }

        public virtual ICollection<proposition> propositions { get; set; }

        public virtual student student { get; set; }

        public virtual user user { get; set; }

        public virtual ICollection<respons> responses { get; set; }
    }
}
