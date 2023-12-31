﻿using System.ComponentModel.DataAnnotations;
using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.ProductDal;

public class Product : EntityBase
{
    [Required]
    public string Ean { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Currency { get; set; }
}
