namespace Task2.ORM.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Task2.ORM.BankModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Task2.ORM.BankModel context)
        {
            context.AccountTypes.AddOrUpdate(
                new AccountType
                {
                    Name = "Base",
                    DepositBonusCost = 30,
                    WithdrawBonusSost = 30,
                },
                new AccountType
                {
                    Name = "Silver",
                    DepositBonusCost = 20,
                    WithdrawBonusSost = 20,
                },
                new AccountType
                {
                    Name = "Gold",
                    DepositBonusCost = 10,
                    WithdrawBonusSost = 10,
                }
                );
        }
    }
}
