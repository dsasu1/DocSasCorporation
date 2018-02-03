CREATE TABLE [dbo].[ZipCodes]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__ZipCodes__Id__740F363E] DEFAULT (newid()),
[Code] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Province] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[County] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CountryCode] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [timestamp] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__ZipCodes__IsVali__75035A77] DEFAULT ((0)),
[Latitude] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Longitude] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ZipCodes__Modifi__75F77EB0] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ZipCodes__AddedD__76EBA2E9] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[ZipCodes] ADD CONSTRAINT [PK__ZipCodes__3214EC06EDBF6C03] PRIMARY KEY NONCLUSTERED  ([Id])
GO
