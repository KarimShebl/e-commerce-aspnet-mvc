﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data.Enums;
using Microsoft.AspNetCore.Identity;
using ecommerce.Models.e_commerce.Models;

namespace ecommerce.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public UserType? Type { get; set; }

    [NotMapped]
    public List<Product>? Products { get; set; }

    [NotMapped]
    public List<Product>? PurchasedProducts { get; set; }
}
