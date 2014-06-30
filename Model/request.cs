namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("prototype.requests")]
    public class request : ViewModel.BaseViewModel
    {
        private string _description;

        public request()
        {
            students = new HashSet<student>();
        }

        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get { return _description; } set { _description = value; NotifyPropertyChanged("description"); } }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string state { get; set; }

        public int users_id { get; set; }

        public virtual user user { get; set; }

        public virtual ICollection<student> students { get; set; }
    }
}
