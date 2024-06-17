using FluentAssertions;
using AlicundeTest.Domain.Models;

namespace AlicundeTest.UnitTest.Banks;

internal class CreateBankTest
{
    [SetUp]
    public void Setup()
    {
    }


    /// <summary>
    /// Example UnitTest
    /// </summary>
    [Test]
    public void CreateBank()
    {
        string name = "Sparebank 1 SMN";
        string bic = "SPTRNO22";
        string country = "NO";

        var bank = Bank.CreateBank(name, bic, country);

        bank.Name.Should().Be(name);
        bank.BIC.Should().Be(bic);
        bank.Country.Should().Be(country);
        bank.Id.Should().NotBeEmpty();
    }
}
