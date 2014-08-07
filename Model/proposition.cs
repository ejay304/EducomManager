using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Proposition.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux propositions (recommandation d'organisation 
    ///     et programme/campus). Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.propositions")]
    public partial class Proposition : NotifyProperty
    {
        private Campus _campu;

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int programs_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int requests_id { get; set; }

        public bool? inscription { get; set; }

        public int? campus_id { get; set; }

        public virtual Campus campu
        {
            get
            {
                return _campu;
            }
            set
            {
                _campu = value;
                NotifyPropertyChanged("campu");
            }
        }

        public virtual Program program { get; set; }

        public virtual Request request { get; set; }
    }
}
