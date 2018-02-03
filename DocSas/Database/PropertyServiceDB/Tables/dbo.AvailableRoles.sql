CREATE TABLE [dbo].[AvailableRoles]
(
[Id] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoleDesc] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ManagementUserId] [uniqueidentifier] NOT NULL,
[HasManagementRights] [bit] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__Available__IsVal__24485945] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Available__Modif__253C7D7E] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Available__Added__2630A1B7] DEFAULT (getutcdate()),
[IsDemoAccount] [bit] NOT NULL CONSTRAINT [DF_AvailableRoles_IsDemoAccount] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[AvailableRoles] ADD CONSTRAINT [PK__Availabl__3214EC064CC38DC4] PRIMARY KEY NONCLUSTERED  ([Id])
GO
