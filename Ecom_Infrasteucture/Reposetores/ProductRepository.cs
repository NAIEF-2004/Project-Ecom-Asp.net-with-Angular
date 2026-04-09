using AutoMapper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Ecom_Core.Services;
using Ecom_Infrasteucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//save
namespace Ecom_Infrasteucture.Reposetores
{
    public  class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        private readonly IImageManagmentService imageManagmentService;

        public ProductRepository(AppDbContext db, IMapper mapper, IImageManagmentService imageManagmentService) :base(db)
        {
            this.db = db;
            this.mapper = mapper;
            this.imageManagmentService = imageManagmentService;

        }

        public async  Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null)  return false;
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var product = mapper.Map<Product>(productDTO);
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();

                var imagepath = await imageManagmentService.Addimage(productDTO.Photos, "ProductImages");
                var photo = imagepath.Select(path => new Photo
                {
                    ImageName = path,
                    ProductId = product.Id,
                }).ToList();

                await db.Photos.AddRangeAsync(photo);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

     

        public async  Task<bool> UpdateAsync(UpdateProductDTO productDTO)
        {
            if (productDTO == null) return false;
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var findproduct=await db.Products.Include(x=>x.category)
                    .Include(x=>x.photos).FirstOrDefaultAsync(x=>x.Id==productDTO.Id);

                if (findproduct == null) return false;

                mapper.Map(productDTO, findproduct);

                //hendling photos
                var findphotos = await db.Photos.Where(x => x.ProductId == productDTO.Id).ToListAsync();

                foreach (var photo in findphotos)
                {
                     imageManagmentService.Deleteimage(photo.ImageName);
                }
                db.Photos.RemoveRange(findphotos);

                var imagepath=await imageManagmentService.Addimage(productDTO.Photos,productDTO.Name);

                var photoNew = imagepath.Select(path => new Photo
                {
                    ImageName = path,
                    ProductId = findproduct.Id,
                }).ToList();
                await db.Photos.AddRangeAsync(photoNew);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task DeleteAsync(Product product)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var photo = await db.Photos.Where(x => x.ProductId == product.Id).ToListAsync();
                foreach (var item in photo) 
                {
                    imageManagmentService.Deleteimage(item.ImageName);
                }
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
