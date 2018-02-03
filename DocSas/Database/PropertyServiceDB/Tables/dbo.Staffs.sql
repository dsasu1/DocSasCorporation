CREATE TABLE [dbo].[Staffs]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[ManagementUserId] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__Staffs__IsValid__3296789C] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Staffs__Modified__338A9CD5] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Staffs__AddedDat__347EC10E] DEFAULT (getutcdate()),
[IsDemoAccount] [bit] NOT NULL CONSTRAINT [DF_Staffs_IsDemoAccount] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[Staffs] ADD CONSTRAINT [PK__Staffs__3214EC068E228E69] PRIMARY KEY NONCLUSTERED  ([Id])
GO
