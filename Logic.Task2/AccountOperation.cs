using System;

namespace Logic.Task2
{
    public static class AccountOperation
    {
        private static AccountService _accountService = new AccountService();

        /// <summary>
        /// Opens account
        /// </summary>
        /// <param name="account"> Account entity </param>
        public static void Open(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            _accountService.Create(account);
        }

        /// <summary>
        /// It is the operation of withdrawing money from the account.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public static void Debit(string accounNumber, decimal value)
        {
            if (string.IsNullOrEmpty(accounNumber))
            {
                throw new ArgumentException("Account number can't be null or empty!");
            }

            Account account = _accountService.GetByValue(accounNumber);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            if (account.InvoiceAmount == 0)
            {
                throw new ArgumentException("The invoice is empty!");
            }

            if (account.InvoiceAmount - value < 0)
            {
                throw new ArgumentException("There is not enough money to perform the operation!");
            }

            account.Bonuses -= (int)(value / account.AccountType.BalanceCost);

            account.InvoiceAmount -= value;

            _accountService.Update(account);
        }

        /// <summary>
        /// It is the operation of account replenishment.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        /// <param name="value"> Amount of money </param>
        public static void Credit(string accounNumber, decimal value)
        {
            if (string.IsNullOrEmpty(accounNumber))
            {
                throw new ArgumentException("Account number can't be null or empty!");
            }

            Account account = _accountService.GetByValue(accounNumber);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            account.Bonuses += (int)(value / account.AccountType.ReplenishmentCost);

            account.InvoiceAmount += value;

            _accountService.Update(account);
        }

        /// <summary>
        /// It is the operation of closing the account.
        /// </summary>
        /// <param name="accounNumber"> Number of account </param>
        public static void Close(string accounNumber)
        {
            if (string.IsNullOrEmpty(accounNumber))
            {
                throw new ArgumentException("Account number can't be null or empty!");
            }

            Account account = _accountService.GetByValue(accounNumber);

            if (account == null)
            {
                throw new ArgumentException("This account doesn't exist!");
            }

            if (account == null)
            {
                throw new ArgumentNullException("Account can't be null!");
            }

            _accountService.Delete(account.Id);
        }
    }
}
