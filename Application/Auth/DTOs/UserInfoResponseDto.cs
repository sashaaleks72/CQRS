
namespace Application.Auth.DTOs
{
    public class UserInfoResponseDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string Gender { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
