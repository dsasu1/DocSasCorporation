CREATE TABLE [dbo].[UserTypes]
(
[Id] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__UserTypes__IsVal__6B79F03D] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__UserTypes__Modif__6C6E1476] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__UserTypes__Added__6D6238AF] DEFAULT (getutcdate()),
[UserTypeEnum] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NoPropertyRedirectPage] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[UserTypes] ADD CONSTRAINT [PK__UserType__3214EC06BDF0C9A5] PRIMARY KEY NONCLUSTERED  ([Id])
GO
