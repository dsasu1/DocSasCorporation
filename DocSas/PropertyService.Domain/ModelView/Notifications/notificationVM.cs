using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyService.Domain.ModelView
{
    /// <summary>
    /// Class NotificationVM.
    /// </summary>
    [Serializable]
    public class NotificationVM
    {
        public Guid Id { get; set; }
        public Guid? PropertyInformationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string NotificationResourceKey { get; set; }
        public string NotificationAdditionalInfo { get; set; }
        public Guid? NotifeeUserId { get; set; }
        public string NotificationShowFor { get; set; }
        public string NotificationTypeEnum { get; set; }
        public Guid? NotificationTypeId { get; set; }
        public DateTime AddedDateUtc { get; set; } 
        public DateTime ModifiedDateUtc { get; set; } 
    }

    /// <summary>
    /// Class NotificationMasterVM.
    /// </summary>
    public class NotificationMasterVM
    {
        public DateTime LastViewDateUTC { get; set; }
        public int NonViewedCount { get; set; }
        public IEnumerable<NotificationVM> NotificationVMS { get; set; }
    }


    /// <summary>
    /// Class NotificationResourceKeys.
    /// </summary>
    public class NotificationResourceKeys
    {
        public const string NotifyNewServiceRequested = "NotifyNewServiceRequested";
        public const string NotifyServiceStatusChanged = "NotifyServiceStatusChanged";
        public const string NotifyNewCommentCardReceieved = "NotifyNewCommentCardReceieved";
        public const string NotifyNewReviewAdded = "NotifyNewReviewAdded";
        public const string NotifyNewResidentRequest = "NotifyNewResidentRequest";
        public const string NotifyResidentMovedOut = "NotifyResidentMovedOut";
        public const string NotifyResidentCancelled = "NotifyResidentCancelled";
        public const string NotifyAddedToStaff = "NotifyAddedToStaff";
        public const string NotifyAddedToRole = "NotifyAddedToRole";
        public const string NotifyAnonymousComment = "NotifyAnonymousComment";
        public const string NotifyInitiatedAMoveout = "NotifyInitiatedAMoveout";
        public const string NotifyApprovedResidency = "NotifyApprovedResidency";
        public const string NotifyDeniedResidency = "NotifyDeniedResidency";
        public const string NotifyCancelledResidency = "NotifyCancelledResidency";
    }
}
