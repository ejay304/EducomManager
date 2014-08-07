using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Campaign.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux campagnes pubicitaires.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>
    
    [Table("EducomDb.campaigns")]
    public partial class Campaign
    {
        public Campaign()
        {
            contacts = new HashSet<Contact>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime creation_date { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string state { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string content { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string comment { get; set; }

        [Column(TypeName = "date")]
        public DateTime? sent_date { get; set; }

        public bool active { get; set; }

        public virtual ICollection<Contact> contacts { get; set; }
    }
}
