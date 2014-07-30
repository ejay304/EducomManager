namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.propositions")]
    public partial class proposition : NotifyProperty
    {
        private campu _campu;

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

        public virtual campu campu
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

        public virtual program program { get; set; }

        public virtual request request { get; set; }
    }
}
