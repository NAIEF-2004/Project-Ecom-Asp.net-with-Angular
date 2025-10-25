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
    public  class PrudactRepostiry: GenricRepository<Prudact>, IPrudactRepostiry
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        private readonly IImageManagmentService imageManagmentService;

        public PrudactRepostiry(AppDbContext db, IMapper mapper, IImageManagmentService imageManagmentService) :base(db)
        {
            this.db = db;
            this.mapper = mapper;
            this.imageManagmentService = imageManagmentService;

        }

        public async  Task<bool> AddAsync(AddprudactDTO prudactDTO)
        {
            if (prudactDTO == null)  return false;
            var prudact=mapper.Map<Prudact>(prudactDTO);
            await db.Prudacts.AddAsync(prudact);
            await db.SaveChangesAsync();

            var imagepath = await imageManagmentService.Addimage(prudactDTO.Photos, "PrudactImages");
            var photo = imagepath.Select(path => new Photo
            {
                ImageName = path,
                PrudactId = prudact.Id,
            }).ToList();

            await db.Photos.AddRangeAsync(photo);
            await db.SaveChangesAsync();
            return true;

        }

        public async  Task<bool> UpdateAsync(UpdateprudactDTO prudactDTO)
        {
            if (prudactDTO == null) return false;

            var findprudact=await db.Prudacts.Include(x=>x.category)
                .Include(x=>x.photos).FirstOrDefaultAsync(x=>x.Id==prudactDTO.Id);

            if (findprudact == null) return false;

            mapper.Map(prudactDTO, findprudact);

            //hendling photos
            var findphotos = await db.Photos.Where(x => x.PrudactId == prudactDTO.Id).ToListAsync();

            foreach (var photo in findphotos)
            {
                 imageManagmentService.Deleteimage(photo.ImageName);
            }
            db.Photos.RemoveRange(findphotos);

            var imagepath=await imageManagmentService.Addimage(prudactDTO.Photos,prudactDTO.Name);

            var photoNew = imagepath.Select(path => new Photo
            {
                ImageName = path,
                PrudactId = findprudact.Id,
            }).ToList();
            await db.Photos.AddRangeAsync(photoNew);
            db.SaveChanges();
            return true;
        }
    }
}
