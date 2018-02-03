SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetResidents]
	-- Add the parameters for the stored procedure here
	@p0 int = null, --recordCount
	@p1 uniqueidentifier = null, --@propertyId
	@p2 uniqueidentifier = null --@UserId
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @p2 IS NULL
	 BEGIN
	   SELECT TOP(@p0)  UNI.Id, UNI.PropertyInformationId,
	    UNI.UserId, UNI.UnitAddress,UNI.UnitName, 
	    UNI.IsDefault , UNI.IsMovedOut, UNI.IsApproved ,
	    US.FirstName + ' ' + ISNULL(US.LastName,'') AS UserName, US.PhotoThumbnail AS UserPhoto,UNI.IsDemoAccount
	   FROM TenantUnits AS UNI (NOLOCK) INNER JOIN 
	       Users AS US (NOLOCK) ON UNI.UserId = US.Id
	   WHERE UNI.PropertyInformationId = @p1
	   ORDER BY UserName
	 END
	 ELSE
	  BEGIN
	    SELECT TOP(@p0)  UNI.Id, UNI.PropertyInformationId,
	    UNI.UserId, UNI.UnitAddress,UNI.UnitName, 
	    UNI.IsDefault , UNI.IsMovedOut, UNI.IsApproved ,
	    US.FirstName + ' ' + ISNULL(US.LastName,'') AS UserName,US.PhotoThumbnail AS UserPhoto,UNI.IsDemoAccount
	   FROM TenantUnits AS UNI (NOLOCK) INNER JOIN 
	       Users AS US (NOLOCK) ON UNI.UserId = US.Id
	   WHERE UNI.PropertyInformationId = @p1
	       AND UNI.UserId = @p2
		ORDER BY UserName
	  END
END
GO
