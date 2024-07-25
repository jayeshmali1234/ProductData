using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductData.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> rate { get; set; }
        public string Total { get; set; }
        public string picname { get; set; }
    }
}