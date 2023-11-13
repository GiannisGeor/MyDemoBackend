namespace Services.Interfaces
{
    public interface ISmtpService
    {
        /// <summary>
        /// Send mail with given configuration
        /// </summary>
        /// <param name="subject">The subject line</param>
        /// <param name="htmlContent">The html formatted content</param>
        /// <param name="textContent">The content in plaintext</param>
        /// <param name="to">List of recipents</param>
        /// <returns></returns>
        public Task SendAsync(string subject, string htmlContent, string textContent, List<string> to);

        /// <summary>
        /// Send mail with given configuration
        /// </summary>
        /// <param name="subject">The subject line</param>
        /// <param name="textContent">The content in plaintext</param>
        /// <param name="to">List of recipents</param>
        /// <returns></returns>
        public Task SendAsync(string subject, string textContent, List<string> to);
    }
}
