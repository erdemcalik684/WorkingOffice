using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet; //sadece burda kullandığım için private
        public Repository(AppDbContext context)
        {
            _context = context; // vt ye erişirim.
            _dbSet = context.Set<TEntity>(); // tablolara erişirim.
        }
        public async Task AddAsync(TEntity entity)
        {
            //awaitin yaptığı iş; bundan sonra yazacağım kod-satır bitene kadar bekle demektir.
            await _dbSet.AddAsync(entity);
        }

        //task ise senkron programlamadaki void'e denk gelir.
        public  async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        //product.Where(x=>x.Name="Erdem");
        //where sorgusu generic bir sorgu olamaz.
        
        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return  await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
            //findasync dizi şeklinde alabilir.
            //almasının sebebi bizim tablolarımızın içerisinde iki tane pk olabilir.
        }


        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //ilk gelen değeri getir yoksa default getir.
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
             _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
