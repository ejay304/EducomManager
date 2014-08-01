using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Respons.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, repr�sentant les donn�es relatives aux r�ponses issues des questionnaires (questionnaires 
    ///     de demandes et de satisfactions). G�n�r�e avec EntityFramework comme couche d'abstraction avec la base de donn�es.
    /// </summary>

    [Table("EducomDb.responses")]
    public partial class Respons
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string response { get; set; }

        public int requests_id { get; set; }

        public int questions_id { get; set; }

        public virtual Question question { get; set; }

        public virtual Request request { get; set; }
    }
}
