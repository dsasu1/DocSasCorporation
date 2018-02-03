CREATE TABLE [dbo].[NewsPosts]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[Details] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[Likes] [int] NOT NULL,
[UnLikes] [int] NOT NULL,
[ShareWithEnum] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__NewsPosts__Modif__67FE6514] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__NewsPosts__Added__68F2894D] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[NewsPosts] ADD CONSTRAINT [PK__NewsPost__3214EC06F6FA60A0] PRIMARY KEY NONCLUSTERED  ([Id])
GO
