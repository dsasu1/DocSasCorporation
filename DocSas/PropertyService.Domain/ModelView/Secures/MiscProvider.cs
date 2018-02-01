using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Extensions;
using DSCAppEssentials.Helpers.DSCEnums;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using System.Linq;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using DSCAppEssentials.Abstract;
using PropertyService.Domain.Managers;
using PropertyService.Domain.ModelView.External;


namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class MiscProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.IMiscProvider" />
    public class MiscProvider : IMiscProvider
    {
        private IPSRepository<SecurityQuestion> _securityQuestion;
        private readonly IPSRepository<Country> _country;
        private readonly IPSRepository<Language> _language;
        private readonly IPSRepository<ZipCode> _zipCode;
        private readonly IMapper _mapper;
        private readonly ISettingProvider _settingProvider;
        /// <summary>
        /// Initializes a new instance of the <see cref="MiscProvider"/> class.
        /// </summary>
        /// <param name="securityQuestion">The security question.</param>
        /// <param name="country">The country.</param>
        /// <param name="language">The language.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="settingProvider">The setting provider.</param>
        /// <param name="mapper">The mapper.</param>
        public MiscProvider(IPSRepository<SecurityQuestion> securityQuestion, IPSRepository<Country> country, IPSRepository<Language> language, IPSRepository<ZipCode> zipCode,  ISettingProvider settingProvider, IMapper mapper)
        {
            _securityQuestion = securityQuestion;
            _country = country;
            _language = language;
            _mapper = mapper;
            _zipCode = zipCode;
            _settingProvider = settingProvider;
        }

        /// <summary>
        /// get security questions as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetSecurityQuestionsAsync()
        {
            var data = await _securityQuestion.GetAsync(x => x.IsValid.ToBool());
         
            return new DSCResponse()
            {
                ResponseData = data
            };
        }

        /// <summary>
        /// get security question as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetSecurityQuestionAsync(Guid id)
        {
            var data = await _securityQuestion.GetAsync(x => x.Id == id);           

            return new DSCResponse()
            {
                ResponseData = data.FirstOrDefault()
            };

        }

        /// <summary>
        /// get countries as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetCountriesAsync()
        {
            var data = await _country.GetAsync(x => x.IsValid.ToBool());

            return new DSCResponse()
            {
                ResponseData = data
            };
        }

        /// <summary>
        /// get zip codes as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetZipCodesAsync()
        {
            var data = await _zipCode.GetAsync(x => x.IsValid.ToBool());

            return new DSCResponse()
            {
                ResponseData = data
            };
        }

        /// <summary>
        /// get languages as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetLanguagesAsync()
        {
            var data = await _language.GetAsync(x => x.IsValid.ToBool());

            return new DSCResponse()
            {
                ResponseData = data
            };
        }

        /// <summary>
        /// get country as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetCountryAsync(Guid id)
        {
            var data = await _country.GetAsync(x => x.Id == id);

            return new DSCResponse()
            {
                ResponseData = data.FirstOrDefault()
            };
        }

        /// <summary>
        /// get country by code as an asynchronous operation.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetCountryByCodeAsync(string code)
        {
            var data = await _country.GetAsync(x => x.Code == code);

            return new DSCResponse()
            {
                ResponseData = data.FirstOrDefault()
            };
        }

        /// <summary>
        /// get language as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetLanguageAsync(Guid id)
        {
            var data = await _language.GetAsync(x => x.Id == id);

            return new DSCResponse()
            {
                ResponseData = data.FirstOrDefault()
            };
        }

        /// <summary>
        /// get zip code as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetZipCodeAsync(Guid id)
        {
            var data = await _zipCode.GetAsync(x => x.Id == id);

            return new DSCResponse()
            {
                ResponseData = data.FirstOrDefault()
            };
        }

        /// <summary>
        /// get zip code by code as an asynchronous operation.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetZipCodeByCodeAsync(string code)
        {
            var data = await _zipCode.GetAsync(x => x.Code == code);

            return new DSCResponse()
            {
                ResponseData = data.FirstOrDefault()
            };
        }

        /// <summary>
        /// look up zip as an asynchronous operation.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> LookUpZipAsync(string code)
        {
            var responseCode = await GetZipCodeByCodeAsync(code);

            if (responseCode.ResponseData == null)
            {
                var url = await _settingProvider.GetSetting(SettingKey.ZipCodeApi);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    url = string.Format(url, code.Trim());

                    var result = await Utility.HttpGetItemAsync(url);

                    if (!string.IsNullOrWhiteSpace(result))
                    {

                        var data = Utility.FromJson<zipData>(result);

                        if (data != null)
                        {
                            var zipCodeEntity = new ZipCode()
                            {
                                Code = data.zip_code,
                                Longitude = data.lng,
                                Latitude = data.lat,
                                City = data.city,
                                Province = data.state
                            };

                            responseCode.ResponseData = zipCodeEntity;
                        }
                    }

                }
            }
          
            return responseCode;
        }
    }
}
