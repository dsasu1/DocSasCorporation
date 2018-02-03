SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetReviews]
	-- Add the parameters for the stored procedure here
	@p0 int = null, --recordCount
	@p1 uniqueidentifier = null, --@propertyId
	@p2 uniqueidentifier = null --@UserId
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

       SELECT TOP(@p0)  REV.Id, US.FirstName + ' ' + LEFT(ISNULL(US.LastName,''),1) AS UserName, 
	   REV.Details, REV.AddedDateUtc, REV.PropertyInformationId, REV.UserId , REV.Title, REV.OverallRating, REV.StaffRating, 
	   REV.NoiseRating, REV.MaintenanceRating, REV.NeighborRating, REV.SafetyRating, REV.GroundsRating, REV.Helpful, REV.UnHelpful	 
	   FROM PropertyReviews AS REV (NOLOCK) INNER JOIN
	   Users AS US (NOLOCK) ON REV.UserId = US.Id
	   WHERE REV.PropertyInformationId = @p1
	   ORDER BY  REV.AddedDateUtc DESC 
	 
	
END
GO
