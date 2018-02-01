using System;
using System.Collections.Generic;
using System.Text;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using System.Linq;
using System.Threading.Tasks;
using DSCAppEssentials.Abstract;

namespace PropertyService.Domain.Providers
{
    /// <summary>
    /// Class SettingsProvider.
    /// </summary>
    /// <seealso cref="DSCAppEssentials.Abstract.ISettingProvider" />
    public class SettingsProvider : ISettingProvider
    {
        private readonly IPSRepository<Setting> _setting;
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsProvider"/> class.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public SettingsProvider(IPSRepository<Setting> setting)
        {
            _setting = setting;
        }

        /// <summary>
        /// GetSetting
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public async Task<string> GetSetting(string settingKey)
        {
            var result = string.Empty;

            var data = await _setting.GetSingleAsync(x => x.SettingKey.Equals(settingKey, StringComparison.OrdinalIgnoreCase));

            if (data != null)
            {
                result = data.SettingValue;
            }

            return result;
        }
    }
}
