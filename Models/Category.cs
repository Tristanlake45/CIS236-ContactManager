using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
