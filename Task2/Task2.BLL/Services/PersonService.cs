using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Task2.BLL.Interface;
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
        private readonly ILogger _logger;
        private static readonly object _syncRoot = new object();
        private static volatile PersonService _instance;
        #endregion

        #region Constructors
        private PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));

            _logger = new Logger();
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

            Person result = NewMethod(serialNumber);

            return result;
        }

        private Person NewMethod(string serialNumber)
        {
            var result = new Person();
            try
            {
                result = _personRepository.GetByPredicate(p => p.SerialNumber == serialNumber).ToPerson();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Returns an entity of <see cref="Person"/>
        /// </summary>
        /// <param name="id"> Person id </param>
        /// <returns> A <see cref="Person" /> object </returns>
        public Person GetPersonById(int id)
        {
            var result = new Person();
            try
            {
                result = _personRepository.GetById(id).ToPerson();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Returns a collection of <see cref="Person"/>
        /// </summary>
        /// <param name="serialNumber"> Passport serial number </param>
        /// <returns> A <see cref="Person" /> object </returns>
        public IEnumerable<Person> GetAll()
        {
            IEnumerable<Person> result = null;
            try
            {
                result = _personRepository.GetAll().ToPersons();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            return result;
        }

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

            bool result = false;
            try
            {
                _personRepository.Create(person.ToDalPerson());
                _unitOfWork.SaveChanges();
                result = true;
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            
            return result;
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

            try
            {
                _personRepository.Update(person.ToDalPerson());
                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
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

            try
            {
                _personRepository.Delete(person.ToDalPerson());
                _unitOfWork.SaveChanges();
            }
            catch (SqlException ex)
            {
                _logger.LogFatal(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
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
