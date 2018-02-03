CREATE TABLE [dbo].[PropertyAccesses]
(
[Id] [uniqueidentifier] NOT NULL,
[RoleId] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__PropertyA__IsVal__6339AFF7] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyA__Modif__642DD430] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyA__Added__6521F869] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[PropertyAccesses] ADD CONSTRAINT [PK__Property__3214EC06738B909A] PRIMARY KEY NONCLUSTERED  ([Id])
GO
