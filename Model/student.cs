namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.students")]
    public partial class student
    {
        public student()
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

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime birthday { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string kinship { get; set; }

        public int contacts_id { get; set; }

        public bool active { get; set; }

        public virtual contact contact { get; set; }

        public virtual ICollection<request> requests { get; set; }
    }

    public sealed class kinship {

        private readonly String name;
        private readonly String value;

        public static readonly kinship father = new kinship("father", "Père");
        public static readonly kinship mother = new kinship ("mother", "Mère");
        public static readonly kinship uncle = new kinship ("uncle", "Oncle"); 
       public static List<kinship> list { get; set; }
        static kinship(){
            list = new List<kinship>();
            list.Add(father);
            list.Add(mother);
            list.Add(uncle);
        }

        private kinship(String value, String name){
            this.name = name;
            this.value = value;
        }

        public override String ToString(){
            return name;
        }
        public String getValue(){
            return name;
        }

    }

   
}
