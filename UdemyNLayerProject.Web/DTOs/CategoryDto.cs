using System.ComponentModel.DataAnnotations;

namespace UdemyNLayerProject.Web.DTOs
{
    /*** DTO Kullanım Amacı ***/
    //DTO kullanılmasının amacı; bizim modellerimiz yada entitylerimiz içerisinde
    //yer alan propertlerin önemli olanlarının clientın bilmesini istemeyiz
    //bu yüzden dto tanımlar ve sadece bilmesi gereken propertileri belirtiriz.
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //isDeleted ve list product nesnelerini görüldüğü gibi çağırmadık.
        //DTO için automapper kullanağız..
    }
}
