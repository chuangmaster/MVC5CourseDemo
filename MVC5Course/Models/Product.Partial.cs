namespace MVC5Course.Models
{
    using MVC5Course.Models.ValidateAttribute;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Stock >= 100 && this.Price < 1000)
            {
                yield return new ValidationResult("當價格低於1000，不能有超過100的庫存!", new string[] { "Stock", "Price" });
            }
        }
    }

    public partial class ProductMetaData 
    {
        
        [Required]
        public int ProductId { get; set; }

        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]

        [商品名稱驗證(ErrorMessage = "商品名稱必要含[商品]")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }

       
    }
}
