CREATE TABLE [dbo].[CommentCards]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[Comment] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[IsAnonymous] [bit] NOT NULL,
[IsValid] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__CommentCa__Modif__2077C861] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__CommentCa__Added__216BEC9A] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[CommentCards] ADD CONSTRAINT [PK__CommentC__3214EC06F96AA655] PRIMARY KEY NONCLUSTERED  ([Id])
GO
