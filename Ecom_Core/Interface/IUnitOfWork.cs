using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Core.Interface
{
    public  interface IUnitOfWork
    {
        public ICategoryRepostiry CategoryRepostiry  { get;  }
        public IPrudactRepostiry PrudactRepostiry { get; }
        public IPhotoRepostiry PhotoRepostiry { get;  }
    }
}
