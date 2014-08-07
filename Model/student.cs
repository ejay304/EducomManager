using PrototypeEDUCOM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Student.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux étudiants.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.students")]
    public class Student : NotifyProperty
    {

        private string _lastname;
        private string _firstname;
        private string _gender;
        private DateTime _birthday;
        private string _kinship;

        public Student()
        {
            requests = new HashSet<Request>();
        }

        public int id { get; set; }

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

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                NotifyPropertyChanged("gender");
            }
        }

        [Column(TypeName = "date")]
        public DateTime birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                NotifyPropertyChanged("birthday");
            }
        }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string kinship
        {
            get
            {
                return _kinship;
            }
            set
            {
                _kinship = value;
                NotifyPropertyChanged("kinship");
            }
        }

        public int contacts_id { get; set; }

        public bool active { get; set; }

        public virtual Contact contact { get; set; }

        public virtual ICollection<Request> requests { get; set; }

        public string fullName
        {
            get
            {
                return this.firstname + " " + this.lastname;
            }
        }

        public override string ToString()
        {
            return fullName;
        }
    }
}
