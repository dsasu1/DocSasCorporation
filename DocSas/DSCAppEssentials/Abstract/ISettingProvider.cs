using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DSCAppEssentials.Abstract
{
    public interface ISettingProvider
    {
        /// <summary>
        /// GetSetting
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        Task<string> GetSetting(string settingKey);
    }
}
