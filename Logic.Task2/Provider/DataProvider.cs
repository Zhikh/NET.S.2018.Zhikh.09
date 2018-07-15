using System.Collections.Generic;

namespace Logic.Task2
{
    public sealed class DataProvider
    {
        private DataProvider()
        {
            Accounts = new List<Account>();
            Persons = new List<Person>();
        }

        public ICollection<Account> Accounts { get; set; }

        public ICollection<Person> Persons { get; set; }

        public static DataProvider Instance { get => Nested.instance; }
        
        private class Nested
        {
            internal static readonly DataProvider instance = new DataProvider();
        }
    }
}
