using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /*UNITOFWORKS UN KULLANILMA AMACI*/
        /*
         * Öncelikle bir projede unitofwork olmazsa ne olur ? 
         * Eğer 3 tablomuz olsun.Bu 3 tablomuza update işlemi yapacağız.
         * 1 ve 2 inci tablolara update işlemi yaptı.
         * Fakat 3 üncü tabloda update yaparken sıkıntı çekti ve problem oldu.
         * Sonucunda ne olacak ? Tekrardan geriye dönük verileri ya silicez,yada düzelticez.
         * Bu bize iş yüküdür.
         * UNITOFWORK Kullanırsak ne olur ?
         * AMACI : Bütün Database işlemleri bize bırakıyor.
         * Yani diyorki sen bütün işlemlerini hallet,ben bunu hafızamda tutuyorum, daha sonra bana haber ver ben onları VT'ye aktaracağım diyor.
         * Yani daha anlaşılı şekilde Commit veya  SavaChanges edeceğim diyor.Amacı budur..
         */

        Task CommitAsync(); // SaveChanges ile aynı anlamdadır...
        void Commit();

        //Bunları vermeyebilirsin...
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }


    }
}
