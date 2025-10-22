using AutoMapper;
using Ecom_Core.DTO;
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
    public  class PrudactRepostiry: GenricRepository<Prudact>, IPrudactRepostiry
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public PrudactRepostiry(AppDbContext db, IMapper mapper) :base(db)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async  Task<bool> AddAsync(AddprudactDTO prudactDTO)
        {
            if (prudactDTO == null)  return false;
            var prudact=mapper.Map<Prudact>(prudactDTO);
            await db.Prudacts.AddAsync(prudact);
            await db.SaveChangesAsync();

        }
    }
}
