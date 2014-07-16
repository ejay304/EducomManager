namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.emails")]
    public partial class email
    {
        public email()
        {
            organisations = new HashSet<organisation>();
        }

        public int id { get; set; }

        [Column("email")]
        [Required]
        [StringLength(45)]
        public string email1 { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        public bool main { get; set; }

        public int contacts_id { get; set; }

        public bool active { get; set; }

        public virtual contact contact { get; set; }

        public virtual ICollection<organisation> organisations { get; set; }
    }
}
