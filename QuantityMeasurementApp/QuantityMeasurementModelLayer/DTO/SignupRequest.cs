using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.DTO;

public class SignupRequest
{
    public string Name { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "email must be in format username@domain.extension")]
    public string Email { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [MaxLength(20, ErrorMessage = "Password cannot not exeed 20 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%&*?])[A-Za-z\d@#$%&*?]{8,20}$", ErrorMessage = "Password must contain lowecase,uppercase,numbers and special characters")]
    public string Password { get; set; }
    public string Role { get; set; } = "user";

   
}