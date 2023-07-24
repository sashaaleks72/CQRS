namespace Infrastructure.Configurations
{
    public class AmazonS3Configuration
    {
        public string Name { get; set; } = string.Empty;

        public Dictionary<string, string> Folders { get; set; } = new Dictionary<string, string>();
    }
}
