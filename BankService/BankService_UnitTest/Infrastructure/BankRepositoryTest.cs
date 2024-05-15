namespace BankService_UnitTest.Infrastructure
{
    using System;
    using System.Xml;
    using BankService_Domain;
    using BankService_Domain.Models;
    using BankService_Helper.DTO;
    using BankService_Infrastructure.Repositories;

    public class BankRepositoryTest
    {
        IBankRepository _bankRepository;

        BankDto _data;

        [SetUp]
        public void Setup()
        {
            _bankRepository = new BankRepository(new BankservicedbMdfContext());

            _data = new BankDto()
            {
                Name = "Banninter",
                Bic = "xx012xx",
                Country = "RD"
            };
        }

        [Test]
        public void AddCorrectResultTest()
        {
            bool result = true;

            try
            {
                _bankRepository.Add(_data).Wait();
            }
            catch (Exception)
            {

                result = false;
            }

            Assert.That(result, Is.True);
        }

        [Test]
        public void AddErrorResultTest()
        {
            bool result = true;

            try
            {
                _bankRepository.Add(new BankDto()).Wait();
            }
            catch (Exception)
            {

                result = false;
            }

            Assert.That(result, Is.False);
        }

        [Test]
        public void GetByCorrectResultTest()
        {
            _bankRepository.Add(_data).Wait();

            BankDto? dto = _bankRepository.GetBy(1).Result;

            Assert.That(dto, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(1));
                Assert.That(_data.Name, Is.EqualTo(dto.Name));
                Assert.That(_data.Country, Is.EqualTo(dto.Country));
                Assert.That(_data.Bic, Is.EqualTo(dto.Bic));
            });

        }
    }
}
