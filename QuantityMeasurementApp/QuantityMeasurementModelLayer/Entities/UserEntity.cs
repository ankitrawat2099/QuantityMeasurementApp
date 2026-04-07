using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; }="user";
    }
}