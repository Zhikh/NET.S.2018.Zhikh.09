using Task2.BLL.Interface.Entities;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Mappers
{
    public static class AccountTypeMappers
    {
        /// <summary>
        /// Converts entity of <see cref="AccountType"/> in <see cref="DalAccountType"/>
        /// </summary>
        /// <param name="accountType"> Entity for converting from <see cref="AccountType"/> </param>
        /// <returns> Entity of  <see cref="DalAccountType"/> </returns>
        public static DalAccountType ToDalAccountType(this AccountType accountType)
        {
            if (accountType == null)
            {
                throw new System.ArgumentNullException(nameof(accountType));
            }

            return new DalAccountType
            {
                BalanceCost = accountType.BalanceCost,
                ReplenishmentCost = accountType.ReplenishmentCost,
                Name = accountType.Name
            };
        }

        /// <summary>
        /// Converts entity of <see cref="DalAccountType"/> in <see cref="AccountType"/>
        /// </summary>
        /// <param name="dalAccountType"> Entity for converting from <see cref="DalAccountType"/> </param>
        /// <returns> Entity of  <see cref="AccountType"/> </returns>
        public static AccountType ToAccountType(this DalAccountType dalAccountType)
        {
            if (dalAccountType == null)
            {
                throw new System.ArgumentNullException(nameof(dalAccountType));
            }

            return new AccountType
            {
                BalanceCost = dalAccountType.BalanceCost,
                ReplenishmentCost = dalAccountType.ReplenishmentCost,
                Name = dalAccountType.Name
            };
        }
    }
}
