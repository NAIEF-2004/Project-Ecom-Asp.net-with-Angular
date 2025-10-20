using Ecom_Core.Interface;
using Ecom_Infrasteucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture.Reposetores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext db;

        public ICategoryRepostiry CategoryRepostiry { get;  }

        public IPrudactRepostiry PrudactRepostiry { get; }

        public IPhotoRepostiry PhotoRepostiry { get; }
        public UnitOfWork(AppDbContext db)
        {
            this.db = db;


            CategoryRepostiry = new CategoryReposetiry(db);
            PrudactRepostiry =new PrudactRepostiry(db);
            PhotoRepostiry=new PhotoRepostiry(db);
        }
    }
}
