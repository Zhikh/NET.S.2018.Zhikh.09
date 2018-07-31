using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class AccountTypeMappers
    {
        public static DalAccountType ToDalAccountType(this AccountType accountType)
        {
            return new DalAccountType
            {
                BalanceCost = accountType.BalanceCost,
                ReplenishmentCost = accountType.ReplenishmentCost,
                Name = accountType.Name
            };
        }

        public static AccountType ToAccountType(this DalAccountType dalAccountType)
        {
            return new AccountType
            {
                BalanceCost = dalAccountType.BalanceCost,
                ReplenishmentCost = dalAccountType.ReplenishmentCost,
                Name = dalAccountType.Name
            };
        }
    }
}
