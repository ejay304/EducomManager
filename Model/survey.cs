namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.surveys")]
    public partial class Survey
    {
        public Survey()
        {
            questions = new HashSet<Question>();
        }

        public int id { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string type { get; set; }

        public virtual ICollection<Question> questions { get; set; }

        public override string ToString()
        {
            return this.type;
        }
    }
}
