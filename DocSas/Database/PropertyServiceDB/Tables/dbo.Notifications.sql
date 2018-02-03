CREATE TABLE [dbo].[Notifications]
(
[Id] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NULL,
[UserId] [uniqueidentifier] NOT NULL,
[NotificationResourceKey] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NotificationAdditionalInfo] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NotifeeUserId] [uniqueidentifier] NULL,
[NotificationTypeEnum] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[NotificationShowFor] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NotificationTypeId] [uniqueidentifier] NULL,
[NotificationProcessed] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Notificat__Modif__16B953FD] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Notificat__Added__17AD7836] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[Notifications] ADD CONSTRAINT [PK__Notifica__3214EC062C644DF8] PRIMARY KEY NONCLUSTERED  ([Id])
GO
