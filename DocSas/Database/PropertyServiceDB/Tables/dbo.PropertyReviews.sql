CREATE TABLE [dbo].[PropertyReviews]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Details] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[OverallRating] [int] NOT NULL,
[StaffRating] [int] NOT NULL,
[NoiseRating] [int] NOT NULL,
[MaintenanceRating] [int] NOT NULL,
[NeighborRating] [int] NOT NULL,
[SafetyRating] [int] NOT NULL,
[GroundsRating] [int] NOT NULL,
[Helpful] [int] NOT NULL,
[UnHelpful] [int] NOT NULL,
[IsValid] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyR__Modif__5AA469F6] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyR__Added__5B988E2F] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[PropertyReviews] ADD CONSTRAINT [PK__Property__3214EC06A155381C] PRIMARY KEY NONCLUSTERED  ([Id])
GO
