CREATE TABLE [dbo].[TenantUnits]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[UnitAddress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UnitName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDefault] [bit] NOT NULL,
[IsApproved] [bit] NULL,
[IsMovedOut] [bit] NOT NULL CONSTRAINT [DF__TenantUni__IsMov__2136E270] DEFAULT ((0)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__TenantUni__Modif__222B06A9] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__TenantUni__Added__231F2AE2] DEFAULT (getutcdate()),
[IsDemoAccount] [bit] NOT NULL CONSTRAINT [DF_TenantUnits_IsDemoAccount] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[TenantUnits] ADD CONSTRAINT [PK__TenantUn__3214EC068C8F081D] PRIMARY KEY NONCLUSTERED  ([Id])
GO
