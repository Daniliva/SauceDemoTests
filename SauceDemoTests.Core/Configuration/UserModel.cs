namespace SauceDemoTests.Core.Configuration
{
    public class UserModel
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ExpectedBadge { get; set; } = "1";

        public bool IsLockedOut { get; set; }
    }
}