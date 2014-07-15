namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Windows.Documents;

    [Table("EducomDb.requests")]
    public partial class request
    {
        public request()
        {
            events = new HashSet<_event>();
            propositions = new HashSet<proposition>();
            responses = new HashSet<respons>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime creation_date { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string comment { get; set; }

        public int students_id { get; set; }

        public int users_id { get; set; }

        public bool active { get; set; }

        public int contacts_id { get; set; }

        public _event state
        {
            get
            {
                _event[] e = new _event[events.Count];
                events.CopyTo(e,0);

                return e[events.Count - 1];
            }
        }

        public virtual contact contact { get; set; }

        public virtual ICollection<_event> events { get; set; }

        public virtual ICollection<proposition> propositions { get; set; }

        public virtual student student { get; set; }

        public virtual user user { get; set; }

        public virtual ICollection<respons> responses { get; set; }
    }
}
