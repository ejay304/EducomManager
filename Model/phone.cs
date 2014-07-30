namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.phones")]
    public partial class Phone
    {
        public Phone()
        {
            organisations = new HashSet<Organisation>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string number { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        public bool main { get; set; }

        public int? contacts_id { get; set; }

        public bool active { get; set; }

        public virtual Contact contact { get; set; }

        public virtual ICollection<Organisation> organisations { get; set; }

        public override string ToString()
        {
            return this.number + " - " + this.description;
        }
    }
}
