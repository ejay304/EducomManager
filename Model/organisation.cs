using PrototypeEDUCOM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
    
namespace PrototypeEDUCOM.Model
{

    /// <filename>Organisation.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux organisations.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.organisation")]
    public partial class Organisation : NotifyProperty
    {

        private string _name;
        private string _street;
        private string _city;
        private string _zip;
        private string _country;

        public Organisation()
        {
            contacts = new HashSet<Contact>();
            programs = new HashSet<Program>();
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

        public virtual ICollection<Contact> contacts { get; set; }

        public virtual Email email { get; set; }

        public virtual Phone phone { get; set; }

        public virtual ICollection<Program> programs { get; set; }
    }
}
