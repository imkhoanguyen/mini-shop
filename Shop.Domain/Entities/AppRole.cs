﻿using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Entities
{
    public class AppRole : IdentityRole
    {
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
