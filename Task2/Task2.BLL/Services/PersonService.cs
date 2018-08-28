using System;
using System.Collections.Generic;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.DAL.Interface;
using Task2.DAL.Interface.Repositories;

namespace Task2.BLL.Services
{
    public class PersonService : IPersonService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        private static readonly object _syncRoot = new object();
        private static volatile PersonService _instance;
        #endregion

        #region Constructors
        private PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        #endregion

        #region Public API
        /// <summary>
        /// Returns an entity of <see cref="Person"/>
        /// </summary>
        /// <param name="serialNumber"> Passport serial number </param>
        /// <returns> A <see cref="Person" /> object </returns>
        public Person GetPerson(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                throw new ArgumentException("message", nameof(serialNumber));
            }

            return _personRepository.GetByPredicate(p => p.SerialNumber == serialNumber).ToPerson();
        }

        /// <summary>
        /// Returns an entity of <see cref="Person"/>
        /// </summary>
        /// <param name="id"> Person id </param>
        /// <returns> A <see cref="Person" /> object </returns>
        public Person GetPersonById(int id) 
            => _personRepository.GetById(id).ToPerson();

        /// <summary>
        /// Returns a collection of <see cref="Person"/>
        /// </summary>
        /// <param name="serialNumber"> Passport serial number </param>
        /// <returns> A <see cref="Person" /> object </returns>
        public IEnumerable<Person> GetAll() 
            => _personRepository.GetAll().ToPersons();

        /// <summary>
        /// Creates the entity of <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Entity of <see cref="Person"/> </param>
        /// <returns> true if the <see cref="Person" /> object was saved; otherwise, false. </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="person"/> is null.
        /// </exception>
        public bool Create(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            Person repoPerson = this.GetPerson(person.SerialNumber);
           
            if (repoPerson != null)
            {
                return false;
            }
            
            _personRepository.Create(person.ToDalPerson());
            _unitOfWork.SaveChanges();

            // TODO: add nornal checking or exclude this !!!!!!!
            return true;
        }

        /// <summary>
        /// Updates entity of <see cref="Person"/> 
        /// </summary>
        /// <param name="person"> Entity of <see cref="Person"/> </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="person"/> is null.
        /// </exception>
        public void Update(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _personRepository.Update(person.ToDalPerson());
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Removes the entity of <see cref="Person"/>
        /// </summary>
        /// <param name="person"> Entity for removing </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="person"/> is null.
        /// </exception>
        public void Delete(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _personRepository.Delete(person.ToDalPerson());
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Returns an object of <see cref="Person"/>
        /// </summary>
        /// <param name="unitOfWork"> Object of <see cref="IUnitOfWork"> </param>
        /// <param name="personRepository"> Object of <see cref="IPersonRepository"> </param>
        /// <returns>  A <see cref="PersonService" /> object </returns>
        /// <exception cref="ArgumentNullException">
        ///      <paramref name="unitOfWork"/> is null.
        ///      <paramref name="personRepository"/> is null.
        /// </exception>
        /// <remarks>
        /// If instance of <see cref="IPersonService"> was created, parameters will not play any roll.
        /// </remarks>
        public static PersonService GetInstance(IUnitOfWork unitOfWork = null, IPersonRepository personRepository = null)
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonService(unitOfWork, personRepository);
                    }
                }
            }

            return _instance;
        }
        #endregion
    }
}
