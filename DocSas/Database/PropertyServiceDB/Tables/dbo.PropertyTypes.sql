CREATE TABLE [dbo].[PropertyTypes]
(
[Id] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__PropertyT__IsVal__1E05700A] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyT__Modif__1EF99443] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyT__Added__1FEDB87C] DEFAULT (getutcdate()),
[PropertyTypeEnum] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
ALTER TABLE [dbo].[PropertyTypes] ADD CONSTRAINT [PK__Property__3214EC069F34FF9B] PRIMARY KEY NONCLUSTERED  ([Id])
GO
