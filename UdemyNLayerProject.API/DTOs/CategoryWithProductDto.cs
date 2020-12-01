using System.Collections.Generic;

namespace UdemyNLayerProject.API.DTOs
{
    public class CategoryWithProductDto : CategoryDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
