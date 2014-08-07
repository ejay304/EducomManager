using PrototypeEDUCOM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Request.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux demandes.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.requests")]
    public partial class Request : NotifyProperty
    {
        private string _journey_type;
        private Student _student;
        public Request()
        {
            events = new HashSet<Event>();
            propositions = new HashSet<Proposition>();
            responses = new HashSet<Respons>();
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

        public Event state
        {
            get
            {
                Event[] e = new Event[events.Count];
                events.CopyTo(e, 0);

                return e[events.Count - 1];
            }

        }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string journey_type { 
            get 
            {
                return _journey_type;
            } 
            set 
            {
                _journey_type = value;
                NotifyPropertyChanged("journey_type");
            }
        }

        public virtual Contact contact { get; set; }

        public virtual ICollection<Event> events { get; set; }

        public virtual ICollection<Proposition> propositions { get; set; }

        public virtual Student student {
            get {
                return _student;
            }
            set {
                _student = value;
                NotifyPropertyChanged("student");
            }
        }

        public virtual User user { get; set; }

        public virtual ICollection<Respons> responses { get; set; }
    }
}
