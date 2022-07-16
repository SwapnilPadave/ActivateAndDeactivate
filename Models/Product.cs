using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCurdApplicationWithRoleBased.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public System.DateTime DateOfPurchase { get; set; }
        public string AvailabilityStatus { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int BrandId { get; set; }
        public bool Active { get; set; }       
        public int Quantity { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
