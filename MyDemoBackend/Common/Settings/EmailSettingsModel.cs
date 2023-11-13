namespace Common.Settings
{
    public class EmailSettingsModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string DisplayAddress { get; set; }

        public string DisplayName { get; set; }

        public string Server { get; set; }

        public bool EnableSSL { get; set; }

        public int Port { get; set; }

        public bool UseOverrideEmail { get; set; }

        public string OverrideAddress { get; set; }

        public bool UseBccAddress { get; set; }

        public string BccAddress { get; set; }
    }
}
