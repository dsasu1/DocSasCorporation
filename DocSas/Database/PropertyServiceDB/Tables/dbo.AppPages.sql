CREATE TABLE [dbo].[AppPages]
(
[Id] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PageDesc] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ComponentName] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PageUrl] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MenuType] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[QueryStringType] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__AppPages__IsVali__2C1E8537] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__AppPages__Modifi__2D12A970] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__AppPages__AddedD__2E06CDA9] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[AppPages] ADD CONSTRAINT [PK__AppPages__3214EC061A5B0320] PRIMARY KEY NONCLUSTERED  ([Id])
GO
