using System.Collections.Generic;

namespace UdemyNLayerProject.Web.DTOs
{
    public class CategoryWithProductDto : CategoryDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
