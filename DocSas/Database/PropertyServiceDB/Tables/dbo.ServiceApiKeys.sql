CREATE TABLE [dbo].[ServiceApiKeys]
(
[Id] [uniqueidentifier] NOT NULL,
[AppApiKey] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AppName] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AppHostName] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AppDomainUrl] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IpAddress] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__ServiceAp__IsVal__511AFFBC] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ServiceAp__Modif__520F23F5] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ServiceAp__Added__5303482E] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[ServiceApiKeys] ADD CONSTRAINT [PK__ServiceA__3214EC06C6DAAC87] PRIMARY KEY NONCLUSTERED  ([Id])
GO
