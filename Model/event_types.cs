namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.event_types")]
    public partial class event_types
    {
        public event_types()
        {
            events = new HashSet<_event>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        public string description { get; set; }

        public short order { get; set; }

        public virtual ICollection<_event> events { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
