CREATE TABLE [dbo].[NotificationViewTracks]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NULL,
[ViewCount] [int] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Notificat__Modif__2AC04CAA] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Notificat__Added__2BB470E3] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[NotificationViewTracks] ADD CONSTRAINT [PK__Notifica__3214EC0664825E54] PRIMARY KEY NONCLUSTERED  ([Id])
GO
