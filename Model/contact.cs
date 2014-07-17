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
        private string _street;
        private string _city;
        private string _zip;
        private string _country;
        private string _district;
        private string _language;
        private DateTime? _add_date;

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
                NotifyPropertyChanged("fullName");
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
                NotifyPropertyChanged("fullName");
            }
        }

        [StringLength(45)]
        public string street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
                NotifyPropertyChanged("street");
            }
        }

        [StringLength(45)]
        public string city
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                NotifyPropertyChanged("city");
            }
        }

        [StringLength(45)]
        public string zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                NotifyPropertyChanged("zip");
            }
        }

        [StringLength(45)]
        public string country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                NotifyPropertyChanged("country");
            }
        }

        [StringLength(45)]
        public string district
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
                NotifyPropertyChanged("country");
            }
        }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                NotifyPropertyChanged("country");
            }
        }

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

        public string fullName
        {
            get
            {
                return this.firstname + " " + this.lastname;
            }
        }
    }
}
