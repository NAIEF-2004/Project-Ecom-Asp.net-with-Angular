using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Entites.Prudact
{
   public  class Category:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public ICollection<Prudact> prudacts { get; set; } = new HashSet<Prudact>();

    }
}
