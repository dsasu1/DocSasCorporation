CREATE TABLE [dbo].[NotificationTemplates]
(
[Id] [uniqueidentifier] NOT NULL,
[TemplateType] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Lang] [nvarchar] (6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TemplateHtml] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TemplatePlainText] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TemplateSubject] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TemplateFromEmail] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TemplateFromName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MediaType] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__Notificat__IsVal__0CDAE408] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Notificat__Modif__0DCF0841] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Notificat__Added__0EC32C7A] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[NotificationTemplates] ADD CONSTRAINT [PK__Notifica__3214EC0664678705] PRIMARY KEY NONCLUSTERED  ([Id])
GO
