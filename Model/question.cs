namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.questions")]
    public partial class Question
    {
        public Question()
        {
            responses = new HashSet<Respons>();
            surveys = new HashSet<Survey>();
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

        public virtual ICollection<Respons> responses { get; set; }

        public virtual ICollection<Survey> surveys { get; set; }
    }
}
