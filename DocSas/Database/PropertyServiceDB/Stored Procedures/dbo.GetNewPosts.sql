SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNewPosts]
	-- Add the parameters for the stored procedure here
	@p0 int = null, --recordCount
	@p1 uniqueidentifier = null, --@propertyId
	@p2 nvarchar(250) = null --@UserType
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	 IF @p2 = 'Tenant'
	  BEGIN
	     SELECT TOP(@p0)  NEWS.Id,
	     CASE WHEN US.UserTypeEnum = 'Tenant' THEN  US.FirstName + ' ' + LEFT(ISNULL(US.LastName,''),1)
		 ELSE PROP.PropName END AS UserName,  US.UserTypeEnum,
		  CASE WHEN US.UserTypeEnum = 'Tenant' THEN  US.PhotoThumbnail
		 ELSE null END AS UserPhoto,  
	   NEWS.Details, NEWS.AddedDateUtc, NEWS.PropertyInformationId , NEWS.UserId , NEWS.Likes , NEWS.Unlikes, NEWS.ShareWithEnum  
	   FROM NewsPosts AS NEWS (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON NEWS.UserId = US.Id
	   INNER JOIN PropertyInformations PROP ON PROP.Id = NEWS.PropertyInformationId
	   WHERE NEWS.PropertyInformationId = @p1
	       AND
	         US.IsActive = 1 AND US.IsBanned = 0
	   ORDER BY  NEWS.AddedDateUtc DESC 
	  END
     ELSE
	  BEGIN
	       SELECT TOP(@p0)  NEWS.Id,
	       US.FirstName + ' ' + LEFT(ISNULL(US.LastName,''),1) AS UserName,  US.UserTypeEnum,
		   US.PhotoThumbnail AS UserPhoto,
	   NEWS.Details, NEWS.AddedDateUtc, NEWS.PropertyInformationId , NEWS.UserId , NEWS.Likes , NEWS.Unlikes, NEWS.ShareWithEnum  
	   FROM NewsPosts AS NEWS (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON NEWS.UserId = US.Id
	   INNER JOIN PropertyInformations PROP ON PROP.Id = NEWS.PropertyInformationId
	   WHERE NEWS.PropertyInformationId = @p1
	       AND
	         US.IsActive = 1 AND US.IsBanned = 0
	   ORDER BY  NEWS.AddedDateUtc DESC 
	  END 

	
END
GO
