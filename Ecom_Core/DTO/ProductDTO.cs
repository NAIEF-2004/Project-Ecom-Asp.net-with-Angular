using Ecom_Core.Entites.Prudact;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.DTO
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal NewPrice { get; set; }
        [Required]
        public decimal OldPrice { get; set; }
        public List<PhotoDTO> Photos { get; set; } = new List<PhotoDTO>();
        [Required]
        public string Categoryname { get; set; }
    }

    public class PhotoDTO
    {
        [Required]
        public string ImageName { get; set; }
        [Required]
        public int ProductId { get; set; }
    }

    public class AddProductDTO 
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal NewPrice { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal OldPrice { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
    }

    public class UpdateProductDTO: AddProductDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
