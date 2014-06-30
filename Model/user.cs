namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.users")]
    public partial class user
    {
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

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string role { get; set; }

        public bool active { get; set; }

        public virtual ICollection<request> requests { get; set; }
    }
}
