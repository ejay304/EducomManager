namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.students")]
    public class student : NotifyProperty
    {

        private string _lastname;
        private string _firstname;
        private string _gender;
        private DateTime _birthday;
        private string _kinship;

        public student()
        {
            requests = new HashSet<request>();
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

        public virtual contact contact { get; set; }

        public virtual ICollection<request> requests { get; set; }
    }
}
