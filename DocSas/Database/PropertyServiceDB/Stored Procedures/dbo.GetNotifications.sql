SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNotifications]
	-- Add the parameters for the stored procedure here
	 @p0 int = null, --recordCount
     @p1 uniqueidentifier = null,--@propertyId
	 @p2 uniqueidentifier = null, --@UserId
	 @p3 nvarchar (250) = null, --@UserType
	 @p4 bit = 0 --@IsManager
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	

	IF @p4 = 1 
	  BEGIN
	   SELECT TOP (@p0 ) NOTI.[Id]
      ,NOTI.[PropertyInformationId]
      ,NOTI.[UserId]
	  ,US.FirstName + ' ' + ISNULL(US.LastName,'') AS UserName
      ,NOTI.[NotificationResourceKey]
      ,NOTI.[NotificationAdditionalInfo]
      ,NOTI.[NotifeeUserId]
      ,NOTI.[NotificationTypeEnum]
      ,NOTI.[NotificationShowFor]
      ,NOTI.[NotificationTypeId]
      ,NOTI.[ModifiedDateUtc]
      ,NOTI.[AddedDateUtc]
        FROM [dbo].[Notifications] AS NOTI WITH (NOLOCK)
		LEFT JOIN Users AS US WITH (NOLOCK) ON NOTI.[UserId] = US.Id
		WHERE ( NOTI.[PropertyInformationId] = @p1 AND NOTI.[NotificationShowFor] IN ('Property' , 'Manager' , 'All')) OR  (NOTI.[NotifeeUserId] = @p2)
	    ORDER BY NOTI.[AddedDateUtc] DESC
	 END
	ELSE IF @p3 = 'ManagementPersonnel'
	  BEGIN
	    SELECT TOP (@p0 ) NOTI.[Id]
      ,NOTI.[PropertyInformationId]
      ,NOTI.[UserId]
	  ,US.FirstName + ' ' + ISNULL(US.LastName,'') AS UserName
      ,NOTI.[NotificationResourceKey]
      ,NOTI.[NotificationAdditionalInfo]
      ,NOTI.[NotifeeUserId]
      ,NOTI.[NotificationTypeEnum]
      ,NOTI.[NotificationShowFor]
      ,NOTI.[NotificationTypeId]
      ,NOTI.[ModifiedDateUtc]
      ,NOTI.[AddedDateUtc]
        FROM [dbo].[Notifications] AS NOTI WITH (NOLOCK)
		INNER JOIN Users AS US WITH (NOLOCK) ON NOTI.[UserId] = US.Id
		WHERE  (NOTI.[PropertyInformationId] = @p1 AND NOTI.[NotificationShowFor] IN ('Property','All'))  OR (NOTI.[NotifeeUserId] = @p2) 
		ORDER BY NOTI.[AddedDateUtc] DESC
	  END
     ELSE IF @p3 = 'Tenant'
	  BEGIN
	    SELECT TOP (100 ) NOTI.[Id]
      ,NOTI.[PropertyInformationId]
	  ,NOTI.[UserId]
	  ,PROP.[PropName] AS UserName
      ,NOTI.[NotificationResourceKey]
      ,NOTI.[NotificationAdditionalInfo]
      ,NOTI.[NotifeeUserId]
      ,NOTI.[NotificationTypeEnum]
      ,NOTI.[NotificationShowFor]
      ,NOTI.[NotificationTypeId]
      ,NOTI.[ModifiedDateUtc]
      ,NOTI.[AddedDateUtc]
        FROM [dbo].[Notifications] AS NOTI WITH (NOLOCK)
		INNER JOIN PropertyInformations AS PROP WITH (NOLOCK) ON NOTI.[PropertyInformationId] = PROP.Id
		WHERE  (NOTI.[PropertyInformationId] = @p1 AND NOTI.[NotificationShowFor] IN ('Residents','All'))  OR (NOTI.[NotifeeUserId] = @p2) 
	    ORDER BY NOTI.[AddedDateUtc] DESC
	  END
	


END
GO
