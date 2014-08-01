using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Question.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux questions issues des questionnaires (questionnaires 
    ///     de demandes et de satisfactions). Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

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
