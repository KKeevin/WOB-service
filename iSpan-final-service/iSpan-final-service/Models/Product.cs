﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace iSpan_final_service.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int ClassId { get; set; }
        public int BrandId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Describe { get; set; }
        public decimal Discount { get; set; }
        public int Stock { get; set; }
        public bool Validity { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}