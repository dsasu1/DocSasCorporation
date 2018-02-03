CREATE TABLE [dbo].[Settings]
(
[Id] [uniqueidentifier] NOT NULL,
[SettingKey] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SettingValue] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Version] [timestamp] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__Settings__IsVali__035179CE] DEFAULT ((1)),
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Settings__Modifi__04459E07] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Settings__AddedD__0539C240] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[Settings] ADD CONSTRAINT [PK__Settings__3214EC06487C853A] PRIMARY KEY NONCLUSTERED  ([Id])
GO
