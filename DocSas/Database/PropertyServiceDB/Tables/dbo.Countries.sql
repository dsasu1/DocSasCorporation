CREATE TABLE [dbo].[Countries]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Countries__Id__119F9925] DEFAULT (newid()),
[Code] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ContinentCode] [nvarchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Version] [timestamp] NOT NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__Countries__IsVal__1293BD5E] DEFAULT ((0)),
[IsLookupEnabled] [bit] NOT NULL,
[IsSupportCounty] [bit] NOT NULL,
[IsSupportProvince] [bit] NOT NULL,
[IsSupportZip] [bit] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Countries__Modif__1387E197] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Countries__Added__147C05D0] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[Countries] ADD CONSTRAINT [PK__Countrie__3214EC0650CD136E] PRIMARY KEY NONCLUSTERED  ([Id])
GO
