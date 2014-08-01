using PrototypeEDUCOM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
    
namespace PrototypeEDUCOM.Model
{

    /// <filename>User.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, repr�sentant les donn�es relatives aux utilisateurs de l'application.
    ///     G�n�r�e avec EntityFramework comme couche d'abstraction avec la base de donn�es.
    /// </summary>

    [Table("EducomDb.users")]
    public partial class User
    {
        private string _role;
        public User()
        {
            requests = new HashSet<Request>();
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
