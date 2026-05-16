using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.DTO
{
    public class CategoryDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        public string? Description { get; set; }
    }

    public class UpdateCategoryDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        public string? Description { get; set; }
    }
}
