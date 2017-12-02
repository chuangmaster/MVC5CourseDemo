using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductListViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "商品名稱不得為空")]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 99999, ErrorMessage = "價格必須介於1~99999")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:#}")]
        public Nullable<decimal> Stock { get; set; }
    }
}