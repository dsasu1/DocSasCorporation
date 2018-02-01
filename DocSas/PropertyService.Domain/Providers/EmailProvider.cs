using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Abstract;
using DSCAppEssentials.Helpers;
using PropertyService.Domain.Managers;
using DSCAppEssentials.Managers;

namespace PropertyService.Domain.Providers
{
    /// <summary>
    /// Class EmailProvider.
    /// </summary>
    /// <seealso cref="DSCAppEssentials.Abstract.IEmailProvider" />
    public class EmailProvider : IEmailProvider
    {
        private readonly ISettingProvider _setting;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProvider"/> class.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public EmailProvider(ISettingProvider setting)
        {
            _setting = setting;
        }

        /// <summary>
        /// send email as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Task.</returns>
        public async Task SendEmailAsync(DSCEmail email)
        {
            var apiKey = await _setting.GetSetting(SettingKey.SendGridApiKey);

            email.EmailApiKey = apiKey;
            await NoficationManager.SendEmailAsync(email);
        }
    }
}
