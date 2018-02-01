using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Helpers;
using Newtonsoft.Json;

namespace DSCAppEssentials.Extensions
{
    /// <summary>
    /// Class Extension.
    /// </summary>
    public static class Extension
    {

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Nulls the check.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public static void NullCheck(this object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }
        }

        #region BOOLEAN        
        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ToBool(this object value)
        {
            if (value == null)
            {
                return false;
            }

            bool result;

            bool.TryParse(value.ToString(), out result);

            return result;
           
        }
        #endregion

        /// <summary>
        /// Joins the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rowId">The row identifier.</param>
        /// <returns>System.String.</returns>
        public static string JoinId(this Guid id, int rowId)
        {
            return string.Concat(id, Utility.Split, rowId);
        }

        #region byte[]        
        /// <summary>
        /// To the base64 string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64String(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }
        #endregion
    }
}
