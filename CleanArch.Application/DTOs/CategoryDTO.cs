using System.ComponentModel.DataAnnotations;

namespace CleanArch.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [MinLength(3, ErrorMessage = "The Name must be at least 3 characters long")]
        [MaxLength(100, ErrorMessage = "The Name cannot exceed 100 characters")]
        public string Name { get; private set; }
    }
}
