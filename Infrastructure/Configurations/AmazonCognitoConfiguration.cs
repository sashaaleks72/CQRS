
namespace Infrastructure.Configurations
{
    public class AmazonCognitoConfiguration
    {
        public string UserPoolId { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;
    }
}
