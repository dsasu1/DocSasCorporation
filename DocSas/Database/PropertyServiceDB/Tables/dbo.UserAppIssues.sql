CREATE TABLE [dbo].[UserAppIssues]
(
[RowId] [int] NOT NULL IDENTITY(1, 1),
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[UserRowId] [int] NOT NULL,
[IssueDesc] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AppBrowser] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [timestamp] NOT NULL,
[IPAddress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__UserAppIs__Modif__2A164134] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__UserAppIs__Added__2B0A656D] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[UserAppIssues] ADD CONSTRAINT [PK__UserAppI__3214EC066947AB65] PRIMARY KEY NONCLUSTERED  ([Id])
GO
