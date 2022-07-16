using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace CoreCurdApplicationWithRoleBased.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [DefaultValue(true)]
        public bool ActiveOrNot { get; set; }
    }
}
