using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
