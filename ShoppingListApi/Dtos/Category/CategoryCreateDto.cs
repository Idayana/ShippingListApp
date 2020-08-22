using System.ComponentModel.DataAnnotations;
using ShoppingListApi.Models;

namespace ShoppingListApi.Dtos.Category
{
    public class CategoryCreateDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}