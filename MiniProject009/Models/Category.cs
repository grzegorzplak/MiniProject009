using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject009.Models
{
    [Table("MiniProject009_Categories")]
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Category name")]
        public string CategoryName { get; set; } = "";
        public ICollection<Expenditure>? Expenditures { get; set; }
    }
}
