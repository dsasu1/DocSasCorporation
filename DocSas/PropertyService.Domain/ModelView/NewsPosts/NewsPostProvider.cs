using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSCAppEssentials.Helpers;
using AutoMapper;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using PropertyService.Domain.Utilities.PSEnums;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class MessageProvider.
    /// </summary>
    /// <seealso cref="PropertyService.Domain.ModelView.INewsPostProvider" />
    public class NewsPostProvider : INewsPostProvider
    {
        private readonly IPSRepository<NewsPost> _newsPostRepo;

        private readonly IMapper _mapper;
        private readonly IAppCommon _appCommon;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsPostProvider"/> class.
        /// </summary>
        /// <param name="newsPostRepo">The news post repo.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appCommon">The application common.</param>
        public NewsPostProvider(IPSRepository<NewsPost> newsPostRepo, IMapper mapper, IAppCommon appCommon)
        {
            _newsPostRepo = newsPostRepo;
            _mapper = mapper;
            _appCommon = appCommon;
        }

        /// <summary>
        /// save news post as an asynchronous operation.
        /// </summary>
        /// <param name="postVM">The post vm.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> SaveNewsPostAsync(PostVM postVM)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();

            var newsPost = _mapper.Map<PostVM, NewsPost>(postVM);

            if (newsPost != null)
            {
                await _newsPostRepo.AddAsync(newsPost);
            }


            response.ResponseData = newsPost;
            response.ErrorMessage = msg;
            return response;
        }

        /// <summary>
        /// delete news post as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="postId">The post identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> DeleteNewsPostAsync(Guid userId, Guid postId)
        {
            var response = new DSCResponse();
            var isSuccess = false;

            var post = await _newsPostRepo.GetSingleAsync(x => x.Id == postId && x.UserId == userId);

            if (post != null)
            {
                isSuccess =  await _newsPostRepo.DeleteAsync(post);
            }

            response.ResponseData = isSuccess;
            return response;
        }

        /// <summary>
        /// get news posts as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns>Task&lt;DSCResponse&gt;.</returns>
        public async Task<DSCResponse> GetNewsPostsAsync(Guid userId, Guid propertyId)
        {
            var msg = new Dictionary<string, string>();
            var response = new DSCResponse();
            IEnumerable<PostVM> news = null;

            var user = await _appCommon.GetUserAsync(userId);

            if (user != null)
            {
                news = await _newsPostRepo.GetQueryAsync<PostVM>("GetNewPosts", new Dictionary<string, object>() {
                        {"recordCount", DataUtil.DefaultNumberOfRows },
                        {"propertyId",propertyId },
                        {"userType",user.UserTypeEnum }
                    });

            }


            response.ResponseData = news;
            return response;
        }
    }
}
