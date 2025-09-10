namespace LOTA.Utility
{
    /// <summary>
    /// SMTP configuration options for email sending
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// SMTP server host (e.g., smtp.gmail.com, smtp.office365.com)
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// SMTP server port (usually 587 for TLS, 465 for SSL, 25 for unencrypted)
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// SMTP username (usually your email address)
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// SMTP password (use app password for Gmail/Outlook)
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Enable SSL/TLS encryption
        /// </summary>
        public bool EnableSsl { get; set; } = true;

        /// <summary>
        /// From email address
        /// </summary>
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// From display name
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
    }
}
