using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using DSCAppEssentials.Managers;


namespace PropertyService.Domain.Managers
{
    /// <summary>
    /// Class SettingKey.
    /// </summary>
    public class SettingKey
    {
        public const string NonReplyEmail = "NonReplyEmail";
        public const string SendGridApiKey = "SendGridApiKey";
        public const string AppUrl = "AppUrl";
        public const string RegisterConfirmUrl = "RegisterConfirmUrl";
        public const string NewPasswordUrl = "NewPasswordUrl";
        public const string ZipCodeApi = "ZipCodeApi";
        public const string RecaptchaUrl = "RecaptchaUrl";
        public const string RecaptchaSecret = "RecaptchaSecret";
        public const string AzureBlobConnectionString = "AzureBlobConnectionString";

    }

    /// <summary>
    /// Class NotifyTemplateKey.
    /// </summary>
    public class NotifyTemplateKey
    {
        public const string RegisterConfirmation = "RegisterConfirmation";
        public const string ForgotPassword = "ForgotPassword";
    }

    /// <summary>
    /// Class SettingManager.
    /// </summary>
    public class SettingManager
    {
        public static string GetPropertyServiceConnection(IConfiguration config , string connectKey)
        {
            return ConfigManager.GetConnectionString(config, connectKey);
        }
    }
}
