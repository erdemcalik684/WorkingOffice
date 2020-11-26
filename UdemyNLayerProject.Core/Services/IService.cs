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
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        //category.SingleOrDefaultAsync(x=>x.name="kalem");
        //1 tane dönüp ilk olanı getirecek...
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity); // Toplu Ekleme İşlemi
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity); // Toplu Silme İşlemi
        TEntity Update(TEntity entity);
    }
}
