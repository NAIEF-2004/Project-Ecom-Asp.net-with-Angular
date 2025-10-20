using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Entites.Prudact
{
    public class Photo:BaseEntity<int>
    {
        public string  ImageName{ get; set; }
        public int PrudactId { get; set; }
        [ForeignKey(nameof(PrudactId))]
        public virtual Prudact prudact { get; set; }
    }
}
