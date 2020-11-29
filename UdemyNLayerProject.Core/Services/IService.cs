using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Core.Services
{
    public interface IService<TEntity> where TEntity:class
    {
        //IRepository deki metotlarla aynısı olacak...
        //İleride sql server değilde başka şeye geçtiğimiz zaman service aynı kalacak sadece repository değişecek çünkü.
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        //find(x=>x.Id=23)
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);

        //category.SingleOrDefaultAsync(x=>x.name="kalem");
        //1 tane dönüp ilk olanı getirecek...
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities); // Toplu Ekleme İşlemi
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities); // Toplu Silme İşlemi
        TEntity Update(TEntity entity);
    }
}
