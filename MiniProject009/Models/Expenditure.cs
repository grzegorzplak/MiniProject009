using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject009.Models
{
    [Table("MiniProject009_Expenditures")]
    public class Expenditure
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Date")]
        [Required]
        public DateOnly ExpenditureDate {  get; set; }

        [DisplayName("Name")]
        [Required]
        public string ExpenditureName { get; set; } = "";

        [DisplayName("Amount")]
        [DataType(DataType.Currency)]
        public decimal ExpenditureAmount { get; set; }

        [DisplayName("Category name")]
        [Required]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }
    }
}
