namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

  
        [Table("EducomDb.contacts")]
        public class contact : NotifyProperty
        {

            private string _firstname;
            private string _lastname;

            public contact()
            {
                emails = new HashSet<email>();
                phones = new HashSet<phone>();
                students = new HashSet<student>();
                campaigns = new HashSet<campaign>();
                requests = new HashSet<request>();
            }

            public int id { get; set; }

            [Column(TypeName = "enum")]
            [StringLength(65532)]
            public string civility { get; set; }

            [Required]
            [StringLength(45)]
            public string firstname
            {
                get
                {
                    return _firstname;
                }
                set
                {
                    _firstname = value;
                    NotifyPropertyChanged("firstname");
                }
            }

            [Required]
            [StringLength(45)]
            public string lastname
            {
                get
                {
                    return _lastname;
                }
                set
                {
                    _lastname = value;
                    NotifyPropertyChanged("lastname");
                }
            }

            [StringLength(45)]
            public string street { get; set; }

            [StringLength(45)]
            public string city { get; set; }

            [StringLength(45)]
            public string zip { get; set; }

            [StringLength(45)]
            public string country { get; set; }

            [StringLength(45)]
            public string district { get; set; }

            [Column(TypeName = "enum")]
            [StringLength(65532)]
            public string language { get; set; }

            [Column(TypeName = "date")]
            public DateTime? add_date { get; set; }

            [StringLength(45)]
            public string situation { get; set; }

            [Column(TypeName = "enum")]
            [StringLength(65532)]
            public string promo_type { get; set; }

            [Column(TypeName = "enum")]
            [StringLength(65532)]
            public string contact_type { get; set; }

            [StringLength(5)]
            public string particule { get; set; }

            public int? programs_id { get; set; }

            public int? organisation_id { get; set; }

            public bool active { get; set; }

            public virtual organisation organisation { get; set; }

            public virtual program program { get; set; }

            public virtual ICollection<email> emails { get; set; }

            public virtual ICollection<phone> phones { get; set; }

            public virtual ICollection<request> requests { get; set; }

            public virtual ICollection<student> students { get; set; }

            public virtual ICollection<campaign> campaigns { get; set; }
        }
}
