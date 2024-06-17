using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlicundeTest.Domain.Abstract;

/// <summary>
/// Class representing a Domain Entity.
/// </summary>
public abstract class Entity<T> where T : Entity<T>
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get; set; }

    public DateTime CreationDate { get; set; }
}
