CREATE TABLE [dbo].[StaffRoles]
(
[Id] [uniqueidentifier] NOT NULL,
[RoleId] [uniqueidentifier] NOT NULL,
[StaffId] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__StaffRole__IsVal__2DD1C37F] DEFAULT ((1)),
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__StaffRole__Modif__2EC5E7B8] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__StaffRole__Added__2FBA0BF1] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[StaffRoles] ADD CONSTRAINT [PK__StaffRol__3214EC06B0CB1FBF] PRIMARY KEY NONCLUSTERED  ([Id])
GO
