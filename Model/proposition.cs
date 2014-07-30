namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
