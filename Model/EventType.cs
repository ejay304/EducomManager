using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>EventType.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, repr�sentant les donn�es relatives aux types d'�v�nement pour les 
    ///     demandes. G�n�r�e avec EntityFramework comme couche d'abstraction avec la base de donn�es.
    /// </summary>

    [Table("EducomDb.event_types")]
    public partial class EventType
    {
        public EventType()
        {
            events = new HashSet<Event>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        public string description { get; set; }

        public short order { get; set; }

        public virtual ICollection<Event> events { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
