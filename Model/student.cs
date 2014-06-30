namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("prototype.students")]
    public partial class student
    {
        public student()
        {
            requests = new HashSet<request>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string firstname { get; set; }

        [Required]
        [StringLength(45)]
        public string lastname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birtday { get; set; }

        public float? wage { get; set; }

        public virtual ICollection<request> requests { get; set; }
    }
}
