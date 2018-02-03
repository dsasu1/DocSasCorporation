CREATE TABLE [dbo].[PropertyEnabledPages]
(
[Id] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[AppPageId] [uniqueidentifier] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__PropertyE__IsVal__3A6CA48E] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyE__Modif__3B60C8C7] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyE__Added__3C54ED00] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[PropertyEnabledPages] ADD CONSTRAINT [PK__Property__3214EC06DA5019CA] PRIMARY KEY NONCLUSTERED  ([Id])
GO
