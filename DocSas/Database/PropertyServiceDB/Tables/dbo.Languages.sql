CREATE TABLE [dbo].[Languages]
(
[Id] [uniqueidentifier] NOT NULL,
[DisplayName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[lang] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TextDirection] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__Languages__IsVal__79C80F94] DEFAULT ((0)),
[IsDefault] [bit] NOT NULL CONSTRAINT [DF__Languages__IsDef__7ABC33CD] DEFAULT ((0)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Languages__Modif__7BB05806] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Languages__Added__7CA47C3F] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[Languages] ADD CONSTRAINT [PK__Language__3214EC06FA7D2132] PRIMARY KEY NONCLUSTERED  ([Id])
GO
