namespace Task2.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account.Account")]
    public partial class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Number { get; set; }

        public int OwnerId { get; set; }

        [Column(TypeName = "money")]
        public decimal Invoice { get; set; }

        public int Bonuses { get; set; }

        public int TypeId { get; set; }

        public virtual AccountType AccountType { get; set; }

        public virtual Person Person { get; set; }
    }
}
