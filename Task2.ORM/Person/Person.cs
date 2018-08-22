namespace Task2.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person.Person")]
    public partial class Person
    {
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Birthday { get; set; }

        public int Id { get; set; }

        public virtual ContactData ContactData { get; set; }

        public virtual Passport Passport { get; set; }
    }
}
