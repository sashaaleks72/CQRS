namespace Application.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string IdToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public string ExpiresAt { get; set; } = string.Empty;
    }
}
