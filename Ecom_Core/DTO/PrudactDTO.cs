using Ecom_Core.Entites.Prudact;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.DTO
{
  public  record PrudactDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<PhotoDTO> photos { get; set; }
        public string Categoryname { get; set; }
    }
    public record PhotoDTO
    {
        public string ImageName { get; set; }
        public int PrudactId { get; set; }
    }
    public record AddprudactDTO 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormCollection Photo { get; set; }
    }
}
