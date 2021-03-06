﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BLL.Interface.Entities;
using Task2.BLL.Interface.Services;
using Task2.BLL.Mappers;
using Task2.BLL.Services;
using Task2.DAL.Interface;
using Task2.DAL.Interface.Repositories;
using Task2.DAL.Interfaces.DTO;

namespace Task2.BLL.Tests.Services
{
    [TestFixture]
    public class PersonServiceTests
    {
        #region Test data
        private IList<Person> _persons = new List<Person>
        {
            new Person
            {
                LastName = "Smbd0",
                FirstName = "Smbd0",
                Email = "podg1@test.com",
                SerialNumber = "12345678FF"
            },
            new Person
            {
                LastName = "Smbd1",
                FirstName = "Smbd1",
                Email = "podg2@test.com",
                SerialNumber = "12345678FA"
            }
        };
        #endregion

        private IPersonService _personService;
        private List<DalPerson> _dalPersons;
        static object _locker = new object();

        [SetUp]
        public void Init()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            _dalPersons = _persons.ToDalPersons().ToList();
            _personService = PersonService.GetInstance(mockUnitOfWork.Object, CreateRepository(_dalPersons));
        }

        #region Exceptions
        [Test]
        public void Create_NullPerson_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => _personService.Create(null));

        [Test]
        public void Update_NullPerson_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => _personService.Update(null));

        [Test]
        public void Delete_NullPerson_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => _personService.Delete(null));

        [Test]
        public void GetPerson_NullString_ArgumentException()
            => Assert.Catch<ArgumentException>(() => _personService.GetPerson(null));
        #endregion

        [Test]
        public void GetAll_Person_CorrectResult()
        {
            foreach (var person in _persons)
            {
                _personService.Create(person);
            }
            
            var actual = _personService.GetAll();
            Assert.AreEqual(_persons, actual);
        }

        [Test]
        public void Create_Person_CorrectResult()
        {
            foreach (var person in _persons)
            {
                _personService.Create(person);
            }

            foreach (var person in _persons)
            {
                Person temp = _personService.GetPerson(person.SerialNumber);
                Assert.AreEqual(person, temp);
            }
        }

        [Test]
        public void Delete_Person_CorrectResult()
        {
            foreach (var person in _persons)
            {
                _personService.Delete(person);
            }

            foreach (var person in _persons)
            {
                Person temp = _personService.GetPerson(person.SerialNumber);
                Assert.AreEqual(true, temp.IsDeleted);
            }
        }

        // FIX: problem with id
        [Test]
        public void Update_Person_CorrectResult()
        {
            foreach (var person in _persons)
            {
                _personService.Create(person);
            }

            foreach (var person in _persons)
            {
                person.LastName += "Test";

                _personService.Update(person);
            }

            foreach (var person in _persons)
            {
                Person temp = _personService.GetPerson(person.SerialNumber);
                Assert.AreEqual(person, temp);
            }
        }

        #region Addition methods
        private IPersonRepository CreateRepository(List<DalPerson> data)
        {
            var mock = new Mock<IPersonRepository>();

            mock.Setup(x => x.Create(It.IsAny<DalPerson>()))
                .Callback(new Action<DalPerson>(x =>
                {
                    lock (_locker)
                    {
                        x.Id = data.Count;
                        data.Add(x);
                    }
                }));

            mock.Setup(x => x.Update(It.IsAny<DalPerson>()))
                .Callback(new Action<DalPerson>(x =>
                {
                    var i = data.FindIndex(q => q.Id.Equals(x.Id));
                    data[i] = x;
                }));

            mock.Setup(x => x.Delete(It.IsAny<DalPerson>()))
                .Callback(new Action<DalPerson>(x =>
                {
                    foreach (var person in data)
                    {
                        if (person.Id == x.Id)
                        {
                            person.IsDeleted = true;
                        }
                    }
                }));

            mock.Setup(x => x.GetAll()).Returns(data);

            mock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => data.Where(
                x => x.Id == i).Single());

            mock.Setup(x => x.GetByPredicate(It.IsAny<Func<DalPerson, bool>>()))
                .Returns((Func<DalPerson, bool> expr) => data.First(expr));

            return mock.Object;
        }
        #endregion
    }
}
