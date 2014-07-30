namespace PrototypeEDUCOM.Model
{
    using PrototypeEDUCOM.Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.organisation")]
    public partial class organisation : NotifyProperty
    {

        private string _name;
        private string _street;
        private string _city;
        private string _zip;
        private string _country;

        public organisation()
        {
            contacts = new HashSet<contact>();
            programs = new HashSet<program>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("name");
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
                return Dictionaries.countries[_country];
            }
            set
            {
                _country = value;
                NotifyPropertyChanged("country");
            }
        }

        public bool active { get; set; }

        public int? phones_id { get; set; }

        public int? emails_id { get; set; }

        public virtual ICollection<contact> contacts { get; set; }

        public virtual email email { get; set; }

        public virtual phone phone { get; set; }

        public virtual ICollection<program> programs { get; set; }
    }
}
