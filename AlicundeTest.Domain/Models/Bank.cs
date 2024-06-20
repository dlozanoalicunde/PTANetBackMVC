using AlicundeTest.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace AlicundeTest.Domain.Models;

public class Bank : Entity<Bank>
{
    public Bank() {}

    public Bank(string name, string bic, string country)
    {
        Id = Guid.NewGuid();
        CreationDateUtc = DateTime.UtcNow;
        Name = name;
        BIC = bic;
        Country = country;
    }

    [MaxLength(400)]
    public string Name { get; set; }

    [MaxLength(11)]
    public string BIC { get; set; }

    [MaxLength(7)]
    public string Country { get; set; }

    public static Bank CreateBank(string name, string bic, string country)
    {
        return new Bank(name, bic, country);
    }
}
