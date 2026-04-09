using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Ecom_Infrasteucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture.Reposetores
{
   public  class PhotoRepository:GenericRepository<Photo>,IPhotoRepository
    {
        public PhotoRepository(AppDbContext db):base(db)
        {
            
        }
    }
}
