using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AutoMapper.Mappers;
using AutoMapper.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.Primitives;
using SixLabors.ImageSharp.Formats;
using System.IO;

namespace DSCAppEssentials.Helpers
{
    public class Utility
    {

        public const string Split = "_";


        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TFrom">The type of the t from.</typeparam>
        /// <typeparam name="TTo">The type of the t to.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>System.Object.</returns>
        public static object Map<TFrom, TTo>(TFrom source)
        {

            var configExp = new MapperConfigurationExpression();
            configExp.CreateMap<TFrom, TTo>();
          

            var config = new MapperConfiguration(configExp);

            IMapper mapper = new Mapper(config);

            var model = mapper.Map<TFrom,TTo>(source);


            return model;                           
                
        }
        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static  string ToJson(object value)
        {
           return  JsonConvert.SerializeObject(value);
        }
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <typeparam name="Tresult">The type of the tresult.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Tresult.</returns>
        public static Tresult FromJson<Tresult>(string value)
        {
            return JsonConvert.DeserializeObject<Tresult>(value);
        }
        /// <summary>
        /// HTTP get item as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public static async Task<string> HttpGetItemAsync(string url)
        {
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            result = data;
                        }
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// HTTP post item as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The post data.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public static async Task<string> HttpPostItemAsync(string url, object postData)
        {
            string result = string.Empty;
            var str = ToJson(postData);
            HttpContent cont = new StringContent(str);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, cont))
                {
                    using (HttpContent content = response.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            result = data;
                        }
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// HTMLs the encode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string HtmlEncode(string value)
        {
            return System.Net.WebUtility.HtmlEncode(value);
        }
        /// <summary>
        /// HTMLs the decode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string HtmlDecode(string value)
        {
            return System.Net.WebUtility.HtmlDecode(value);
        }
        /// <summary>
        /// Croppings the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] Cropping(byte[] stream, int width, int height) {
            byte[] output = null;
            IImageFormat format;

            var image = Image.Load<Rgba32>(stream, out format);
            
                image.Mutate(x => x
                     .Resize(width, height).Grayscale());
                output = image.SavePixelData();
            

            return output;

        }


        public static string GetShortTen(string value)
        {
            var result = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                result = value.Length > 10 ? string.Concat(value.Substring(0, 10), "...") : value;
            }

            return result;
        }

    }


}
