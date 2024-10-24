namespace API.Configurations
{
    public class EmailConfig
    {
        public required string DefaultSender { get; set; }
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
        public required string Provider { get; set; }
        public required int Port { get; set; }
    }
}
