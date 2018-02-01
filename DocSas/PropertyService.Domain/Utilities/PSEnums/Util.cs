using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.Utilities.PSEnums
{
    /// <summary>
    /// Class DataUtil.
    /// </summary>
    public class DataUtil
    {
        public const int DefaultNumberOfRows = 1000;
    }

    /// <summary>
    /// Enum PSChangeType
    /// </summary>
    public enum PSChangeType
    {
        SecurityQuestion,
        PersonalInfo,
        PasswordChange,
        UserProfileImage,

    }

    /// <summary>
    /// Enum PSUploadType
    /// </summary>
    public enum PSUploadType
    {
        CoverImage,
        Photos,
        Forms,
        FloorPlans,
        ProfilePic

    }

    /// <summary>
    /// Enum PSFileType
    /// </summary>
    public enum PSFileType
    {
        image
    }

    /// <summary>
    /// Enum PSUserType
    /// </summary>
    public enum PSUserType
    {
        Tenant,
        ManagementCompany,
        ManagementPersonnel
    }

    public enum PSResidencyChangeType
    {
        MoveOut,
        Approval,
        Cancel
    }

    public enum PSNotificationType
    {
        ServiceRequest,
        Residents,
        CommentCard,
        Review,
        Role, 
        Staff,
        Event,
        NewsPost,
        Property

    }

    public enum PSNotificationShowFor
    {
        All,
        Property,
        Manager,
        Residents,
        User

    }

}
