namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.questions")]
    public partial class question
    {
        public question()
        {
            responses = new HashSet<respons>();
            surveys = new HashSet<survey>();
        }

        public int id { get; set; }

        [Column("question", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string question1 { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string type { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string choice { get; set; }

        public bool active { get; set; }

        public virtual ICollection<respons> responses { get; set; }

        public virtual ICollection<survey> surveys { get; set; }
    }
}
