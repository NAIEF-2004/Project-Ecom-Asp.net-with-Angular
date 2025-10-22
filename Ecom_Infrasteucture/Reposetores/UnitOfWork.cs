using AutoMapper;
using Ecom_Core.Interface;
using Ecom_Core.Services;
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
        private readonly IMapper mp;
        private readonly IImageManagmentService imageManagmentService;

        public ICategoryRepostiry CategoryRepostiry { get;  }

        public IPrudactRepostiry PrudactRepostiry { get; }

        public IPhotoRepostiry PhotoRepostiry { get; }
        public UnitOfWork(AppDbContext db,IMapper mp,IImageManagmentService imageManagmentService)
        {
            this.db = db;
            this.mp = mp;
            this.imageManagmentService = imageManagmentService;
            CategoryRepostiry = new CategoryReposetiry(db);
            PrudactRepostiry =new PrudactRepostiry(db,mp, imageManagmentService);
            PhotoRepostiry=new PhotoRepostiry(db);
        }
    }
}
