CREATE TABLE [dbo].[ServiceRequests]
(
[Id] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Details] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Phone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GivePermission] [bit] NOT NULL,
[HavePet] [bit] NOT NULL,
[HaveAlarm] [bit] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[TenantUnitId] [uniqueidentifier] NOT NULL,
[TenantUnitAddress] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[RequestStatusKey] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ServiceRe__Modif__18D6A699] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ServiceRe__Added__19CACAD2] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[ServiceRequests] ADD CONSTRAINT [PK__ServiceR__3214EC06B87A07BA] PRIMARY KEY NONCLUSTERED  ([Id])
GO
