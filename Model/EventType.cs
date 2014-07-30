namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
