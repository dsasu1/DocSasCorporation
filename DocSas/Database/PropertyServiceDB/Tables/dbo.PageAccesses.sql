CREATE TABLE [dbo].[PageAccesses]
(
[Id] [uniqueidentifier] NOT NULL,
[RoleId] [uniqueidentifier] NOT NULL,
[AppPageId] [uniqueidentifier] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__PageAcces__IsVal__30E33A54] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PageAcces__Modif__31D75E8D] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PageAcces__Added__32CB82C6] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[PageAccesses] ADD CONSTRAINT [PK__PageAcce__3214EC064F883029] PRIMARY KEY NONCLUSTERED  ([Id])
GO
