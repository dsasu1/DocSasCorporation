SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetServiceRequests]
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
       SELECT TOP(@p0) SR.Id, SR.Title, US.FirstName + ' ' + ISNULL(US.LastName, '') AS UserName, SR.Details, SR.Phone ,SR.GivePermission,
	        SR.HavePet, SR.HaveAlarm, SR.UserId, SR.PropertyInformationId, SR.TenantUnitId, SR.TenantUnitAddress, SR.RequestStatusKey, SR.AddedDateUtc
	   FROM ServiceRequests AS SR (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON SR.UserId = US.Id
	   WHERE SR.PropertyInformationId = @p1
	   ORDER BY  SR.AddedDateUtc DESC 
	 END
	 ELSE
	  BEGIN
	   SELECT TOP(@p0) SR.Id, SR.Title, US.FirstName + ' ' + ISNULL(US.LastName, '') AS UserName,SR.Details, SR.Phone ,SR.GivePermission,
	        SR.HavePet, SR.HaveAlarm, SR.UserId, SR.PropertyInformationId,SR.TenantUnitId,SR.RequestStatusKey, SR.TenantUnitAddress, SR.AddedDateUtc
	   FROM ServiceRequests AS SR (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON SR.UserId = US.Id
	   WHERE SR.PropertyInformationId = @p1 AND SR.UserId = @p2
	   ORDER BY  SR.AddedDateUtc DESC 

	  END
END
GO
