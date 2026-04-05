using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecom_Core.DTO
{
    public record dtoAccountUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required,MinLength(6)]

        public int Password { get; set; }
        public string? Phone { get; set; }

    }
}
