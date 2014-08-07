using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Event.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux événement d'une demande.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.events")]
    public partial class Event
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string comment { get; set; }

        public int event_types_id { get; set; }

        public int requests_id { get; set; }

        public virtual EventType event_types { get; set; }

        public virtual Request request { get; set; }
        public override string ToString()
        {
            return this.event_types.name;
        }

    }
}
