namespace PrototypeEDUCOM.Model
{
    using PrototypeEDUCOM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

    [Table("EducomDb.users")]
    public partial class user
    {
        private string _role;
        public user()
        {
            requests = new HashSet<request>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string lastname { get; set; }

        [Required]
        [StringLength(45)]
        public string firstname { get; set; }

        [Required]
        [StringLength(45)]
        public string email { get; set; }

        [Required]
        [StringLength(45)]
        public string password { get; set; }

        [Required]
        public string salt { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string role {
            get { 
                return Dictionaries.users[_role];
            }
            set {
                _role = value;
            } 
        }

        public bool active { get; set; }

        public virtual ICollection<request> requests { get; set; }

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
