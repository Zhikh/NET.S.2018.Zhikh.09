using System.Collections.Generic;
using Task2.BLL.Interface.Entities;

namespace Task2.BLL.Interface.Services
{
    public interface IPersonService
    {
        /// <summary>
        /// Creates the entity of <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Entity of <see cref="Person"/> </param>
        /// <returns> true if the <see cref="Person" /> object was saved; otherwise, false. </returns>
        bool Create(Person person);

        /// <summary>
        /// Returns an entity of <see cref="Person"/>
        /// </summary>
        /// <param name="serialNumber"> Passport serial number </param>
        /// <returns> A <see cref="Person" /> object </returns>
        Person GetPerson(string serialNumber);

        /// <summary>
        /// Returns an entity of <see cref="Person"/>
        /// </summary>
        /// <param name="id"> Person id </param>
        /// <returns> A <see cref="Person" /> object </returns>
        Person GetPersonById(int id);

        /// <summary>
        /// Returns a collection of <see cref="Person"/>
        /// </summary>
        /// <returns> A <see cref="Person" /> object </returns>
        IEnumerable<Person> GetAll();

        /// <summary>
        /// Updates entity of <see cref="Person"/> 
        /// </summary>
        /// <param name="person"> Entity of <see cref="Person"/> </param>
        void Update(Person person);

        /// <summary>
        /// Removes the entity of <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Entity for removing </param>
        void Delete(Person person);
    }
}
