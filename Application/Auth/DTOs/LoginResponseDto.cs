namespace Application.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string IdToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}
