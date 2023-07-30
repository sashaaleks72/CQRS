
namespace Application.Auth.DTOs
{
    public class RegisterRequestDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string Gender { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
