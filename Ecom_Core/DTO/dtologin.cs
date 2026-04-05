using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Ecom_Core.DTO
{
    public  record dtologin
    {
        [Required]
        public int password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
