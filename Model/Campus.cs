using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace PrototypeEDUCOM.Model
{

    /// <filename>Campus.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux campus.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.campus")]
    public partial class Campus
    {
        public Campus()
        {
            propositions = new HashSet<Proposition>();
            programs = new HashSet<Program>();
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

        public virtual ICollection<Proposition> propositions { get; set; }

        public virtual ICollection<Program> programs { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
