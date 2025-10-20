using Ecom_Core.Interface;
using Ecom_Infrasteucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture.Reposetores
{
    public class GenricRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly AppDbContext db;

        public GenricRepository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(T item)
        {
           await db.Set<T>().AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
        var d=await db.Set<T>().FindAsync(id);
            db.Set<T>().Remove(d);
            await db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            =>await db.Set<T>().AsNoTracking().ToListAsync();//مشان ما يحصل اي تعديلات على الداتا اذناء الجلب ومشان السرعة

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includs)
        {
           var q=db.Set<T>().AsQueryable();//اجضار من الداتا باس
            foreach (var item in includs) { 
            q=q.Include(item);
            }
            return await q.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var a = await db.Set<T>().FindAsync(id);
            return a;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includs)
        {
            IQueryable<T> query = db.Set<T>();

            foreach (Expression<Func<T, object>> item in includs)
            {
                query = query.Include(item);
            }

            var entity = await query.FirstOrDefaultAsync(r => EF.Property<int>(r, "Id") == id);
            return entity;

        }

        public Task UpdateAsync(T item)
        {
            db.Entry(item).State = EntityState.Modified;
            return db.SaveChangesAsync();
        }
    }
}
