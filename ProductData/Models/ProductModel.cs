using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductData.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Name is Required...")]
        public string name { get; set; }
        [Required(ErrorMessage ="City is Required...")]
        public string city { get; set; }
        [Required(ErrorMessage ="Quntity is Required...")]
        public Nullable<int> Qty { get; set; }
        public Nullable<int> rate { get; set; }
        public string Total { get; set; }
        public string picname { get; set; }
    }
}