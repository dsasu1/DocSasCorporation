using System;
using System.Collections.Generic;
using System.Text;
using DSCAppEssentials.Helpers.DSCEnums;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using PropertyService.Domain.DataEntities;
using PropertyService.Domain.DataBaseContext;
using System.Linq;

namespace PropertyService.Domain.ModelView
{
    public class CodeGeneratorProvider : ICodeGeneratorProvider
    {
        private readonly IPSRepository<ServiceCodeTrack> _serviceTrackRepo;
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorProvider"/> class.
        /// </summary>
        /// <param name="serviceTrackRepo">The service track repo.</param>
        public CodeGeneratorProvider(IPSRepository<ServiceCodeTrack> serviceTrackRepo)
        {
            _serviceTrackRepo = serviceTrackRepo;
        }

        /// <summary>
        /// generate code as an asynchronous operation.
        /// </summary>
        /// <param name="cType">Type of the c.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async  Task<DSCResponse> GenerateCodeAsync(DSCCodeType cType)
        {
            var response = new DSCResponse();
            var msg = new Dictionary<string, string>();

            var serviceTrack = await _serviceTrackRepo.GetSingleAsync(x => x.CodeType.Equals(cType.ToString(), StringComparison.OrdinalIgnoreCase));
            var result = string.Empty;

            if (serviceTrack != null)
            {

                var letters = serviceTrack.AvailableLetters;
                var lettersLength = serviceTrack.LettersLength;
                var start = serviceTrack.StartValue;
                var max = serviceTrack.MaxInd;

                Enum.TryParse(serviceTrack.NumberPosition, out DSCPosition numPosition);

                for (int i = 0; i < lettersLength; i++)
                {
                    Random rnd = new Random();
                    var index = rnd.Next(letters.Length);

                    result += letters[index];
                }

                switch (numPosition)
                {
                    case DSCPosition.Right:
                        result = string.Concat(result, max);
                        break;
                    case DSCPosition.Left:
                        result = string.Concat(max, result);
                        break;
                }

                serviceTrack.MaxInd++;

                var success = await _serviceTrackRepo.ModifyAsync(serviceTrack);

                if (!success)
                {
                    msg.Add("SomethingWrong", "Something went wrong");
                }

            }

            response.ResponseData = result;
            response.ErrorMessage = msg;
            return response;
        }
    }
}
