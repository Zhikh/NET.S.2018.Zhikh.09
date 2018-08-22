namespace Task2.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person.ContactData")]
    public partial class ContactData
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public virtual Person Person { get; set; }
    }
}
