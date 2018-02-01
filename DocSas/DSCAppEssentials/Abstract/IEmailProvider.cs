using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;

namespace DSCAppEssentials.Abstract
{
    public interface IEmailProvider
    {
        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendEmailAsync(DSCEmail email);
    }
}
