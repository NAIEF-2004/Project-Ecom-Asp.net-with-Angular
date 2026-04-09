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

        public ICategoryRepository CategoryRepository { get;  }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }
        public UnitOfWork(AppDbContext db,IMapper mp,IImageManagmentService imageManagmentService)
        {
            this.db = db;
            this.mp = mp;
            this.imageManagmentService = imageManagmentService;
            CategoryRepository = new CategoryRepository(db);
            ProductRepository =new ProductRepository(db,mp, imageManagmentService);
            PhotoRepository=new PhotoRepository(db);
        }
    }
}
