SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCommentCards]
	-- Add the parameters for the stored procedure here
	@p0 int = null, --recordCount
	@p1 uniqueidentifier = null, --@propertyId
	@p2 uniqueidentifier = null --@UserId
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @p2 IS NULL
	 BEGIN
       SELECT TOP(@p0) COMM.Id, CASE WHEN  
	             COMM.IsAnonymous = 1 THEN 'Anonymous'  
				 ELSE US.FirstName + ' ' + ISNULL(US.LastName,'')
				 END  AS UserName
				 , CASE WHEN  
	             COMM.IsAnonymous = 1 THEN null 
				 ELSE US.PhotoThumbnail 
				 END  AS UserPhoto
	   , COMM.Comment, COMM.IsAnonymous, COMM.AddedDateUtc, COMM.PropertyInformationId , COMM.UserId 
	   FROM CommentCards AS COMM (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON COMM.UserId = US.Id
	   WHERE COMM.PropertyInformationId = @p1
	   ORDER BY  COMM.AddedDateUtc DESC 
	 END
	 ELSE
	  BEGIN
	     SELECT TOP(@p0) COMM.Id,  US.FirstName + ' ' + ISNULL(US.LastName,'') AS UserName
				 ,US.PhotoThumbnail  AS UserPhoto
	   , COMM.Comment, COMM.IsAnonymous, COMM.AddedDateUtc, COMM.PropertyInformationId , COMM.UserId 
	   FROM CommentCards AS COMM (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON COMM.UserId = US.Id
	   WHERE COMM.PropertyInformationId = @p1 AND COMM.UserId = @p2
	   ORDER BY  COMM.AddedDateUtc DESC 
	  END
END
GO
