SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStaffs]
	-- Add the parameters for the stored procedure here
	@p0 int = null, --recordCount
	@p1 uniqueidentifier = null --@managerUserId
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	     SELECT  STF.Id,
		         SR.Id AS StaffRoleId,
	             US.FirstName + ' ' + ISNULL(US.LastName,'') AS UserName,
				 US.PhotoThumbnail AS UserPhoto
	   , STF.UserId ,
	   STF.Title, SR.RoleId, AVR.Title AS [RoleName], STF.IsDemoAccount
	   FROM Staffs AS STF (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON STF.UserId = US.Id
	   LEFT JOIN StaffRoles SR ON STF.Id = SR.StaffId
	   LEFT JOIN AvailableRoles AVR ON SR.RoleId = AVR.Id
	   WHERE STF.ManagementUserId = @p1 AND
	         US.IsActive = 1 AND US.IsBanned = 0
	   ORDER BY UserName 
	  
END
GO
