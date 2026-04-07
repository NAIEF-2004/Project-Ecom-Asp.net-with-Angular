using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Ecom_Core.DTO
{
    public  record dtoLoginUser
    {
        [Required]
        public string password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
