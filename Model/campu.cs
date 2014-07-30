namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.campus")]
    public partial class campu
    {
        public campu()
        {
            propositions = new HashSet<proposition>();
            programs = new HashSet<program>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        public string street { get; set; }

        [StringLength(45)]
        public string city { get; set; }

        [StringLength(45)]
        public string zip { get; set; }

        [StringLength(45)]
        public string country { get; set; }

        public bool active { get; set; }

        public virtual ICollection<proposition> propositions { get; set; }

        public virtual ICollection<program> programs { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
