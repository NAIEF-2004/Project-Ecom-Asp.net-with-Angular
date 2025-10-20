using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Entites.Prudact
{
    public class Prudact:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<Photo> photos { get; set; }
        public int  CategoryId{ get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category category { get; set; }
    }
}
