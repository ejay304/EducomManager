namespace PrototypeEDUCOM.Model
{
    using PrototypeEDUCOM.Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.program_types")]
    public partial class program_types
    {

        public program_types()
        {
            programs = new HashSet<program>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        public virtual ICollection<program> programs { get; set; }
        
        public override string ToString()
        {
            return this.description;
        }

    }
}
