namespace Task2.ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BankModel : DbContext
    {
        public BankModel()
            : base("name=BankModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ContactData> ContactDatas { get; set; }
        public virtual DbSet<Passport> Passports { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Invoice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AccountType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AccountType>()
                .Property(e => e.DepositBonusCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AccountType>()
                .Property(e => e.WithdrawBonusSost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<AccountType>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.AccountType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactData>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Passport>()
                .Property(e => e.PassportSeries)
                .IsFixedLength();

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.ContactData)
                .WithRequired(e => e.Person);

            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Passport)
                .WithRequired(e => e.Person);
        }
    }
}
