namespace Task2.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person.Passport")]
    public partial class Passport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonId { get; set; }

        [Required]
        [StringLength(10)]
        public string PassportSeries { get; set; }

        [Required]
        [StringLength(10)]
        public string Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Authority { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime IssueDate { get; set; }

        [Required]
        [StringLength(50)]
        public string IdNumber { get; set; }

        public virtual Person Person { get; set; }
    }
}
