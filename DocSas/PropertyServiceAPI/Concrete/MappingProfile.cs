using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyService.Domain.ModelView;
using PropertyService.Domain.DataEntities;
using DSCAppEssentials.Helpers;

namespace PropertyServiceAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserType,UserTypeVM>();
            CreateMap<UserTypeVM, UserType>();
            CreateMap<IList<UserType>, IList<UserTypeVM>>();
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
            CreateMap<Language, LanguageVM>();
            CreateMap<IList<Language>, IList<LanguageVM>>();
            CreateMap<PropertyInformationVM, PropertyInformation>();
            CreateMap<PropertyInformation, PropertyInformationVM>().ForMember(dest=> dest.AboutUs, opt=> opt.ResolveUsing(new CustomPropertyInformationVMResolver()));
            CreateMap<IList<PropertyInformation>, IList<PropertyInformationVM>>();
            CreateMap<CommentCardVM, CommentCard>();
            CreateMap<CommentCard, CommentCardVM>();
            CreateMap<IList<CommentCard>, IList<CommentCardVM>>();
            CreateMap<ServiceRequestVM, ServiceRequest>();
            CreateMap<ServiceRequest, ServiceRequestVM>();
            CreateMap<IList<ServiceRequest>, IList<ServiceRequestVM>>();
            CreateMap<OperationHourVM, OperationHour>();
            CreateMap<OperationHour, OperationHourVM>();
            CreateMap<IList<OperationHour>, IList<OperationHourVM>>();
            CreateMap<IList<OperationHourVM>, IList<OperationHour>>();
            CreateMap<PostVM, NewsPost>();
            CreateMap<NewsPost, PostVM>();
            CreateMap<IList<NewsPost>, IList<PostVM>>();
            CreateMap<IList<PostVM>, IList<NewsPost>>();
            CreateMap<EventVM, PropertyEvent>();
            CreateMap<PropertyEvent, EventVM>();
            CreateMap<IList<PropertyEvent>, IList<EventVM>>();
            CreateMap<IList<EventVM>, IList<PropertyEvent>>();
            CreateMap<ReviewVM, PropertyReview>();
            CreateMap<PropertyReview, ReviewVM>();
            CreateMap<IList<PropertyReview>, IList<ReviewVM>>();
            CreateMap<IList<ReviewVM>, IList<PropertyReview>>();
            CreateMap<AvailableRoleVM, AvailableRole>();
            CreateMap<AvailableRole, AvailableRoleVM>();
            CreateMap<IList<AvailableRole>, IList<AvailableRoleVM>>();
            CreateMap<IList<AvailableRoleVM>, IList<AvailableRole>>();
            CreateMap<NotificationVM, Notification>();
            CreateMap<Notification, AvailableRoleVM>();

        }
    }

    public class CustomPropertyInformationVMResolver : IValueResolver<object, object, string>
    {
        public string Resolve(object source, object destination,string destinationMember, ResolutionContext context)
        {
            return Utility.HtmlDecode(((PropertyInformation)source).AboutUs);
        }
    }
}
