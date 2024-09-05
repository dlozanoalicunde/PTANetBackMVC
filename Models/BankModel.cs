using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models
{
    public class BankModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankID { get; set; }
        public string Bic { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }

    }
}