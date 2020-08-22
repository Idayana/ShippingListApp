using System.ComponentModel.DataAnnotations;
using ShoppingListApi.Models;

namespace ShoppingListApi.Dtos.Category
{
    public class CategoryUpdateDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}