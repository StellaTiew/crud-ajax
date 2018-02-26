using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeysOnboarding2.Models
{
    public class ViewModel
    {

        public class ProductViewModel
        {
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }
            public Nullable<decimal> Price { get; set; }

            public virtual ICollection<ProductSold> ProductSolds { get; set; }
        }

        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Nullable<int> Age { get; set; }
            public string Address { get; set; }

            public virtual ICollection<ProductSold> ProductSolds { get; set; }
        }

        public class StoreViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public virtual ICollection<ProductSold> ProductSolds { get; set; }
        }

        public class SalesViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public int CustomerId { get; set; }
            public int StoreId { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> DateSold { get; set; }

            public virtual Customer Customer { get; set; }
            public virtual Product Product { get; set; }
            public virtual Store Store { get; set; }

            public string ProductName { get; set; }
            public string CustomerName { get; set; }
            public string StoreName { get; set; }
        }
        
    }
}