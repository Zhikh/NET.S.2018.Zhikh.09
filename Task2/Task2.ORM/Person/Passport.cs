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

        public virtual Person Person { get; set; }
    }
}
