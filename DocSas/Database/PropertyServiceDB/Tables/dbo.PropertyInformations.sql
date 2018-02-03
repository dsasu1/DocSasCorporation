CREATE TABLE [dbo].[PropertyInformations]
(
[Id] [uniqueidentifier] NOT NULL,
[ManagementUserId] [uniqueidentifier] NOT NULL,
[PropName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UrlFriendlyName] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PropCode] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PropType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AboutUs] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StreetOne] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StreetTwo] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ZipId] [uniqueidentifier] NOT NULL,
[Phone] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Fax] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Weburl] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PropTimezone] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CoverOriginal] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CoverThumbnail] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsValid] [bit] NOT NULL CONSTRAINT [DF__PropertyI__IsVal__095F58DF] DEFAULT ((1)),
[IsLive] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyI__Modif__0A537D18] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyI__Added__0B47A151] DEFAULT (getutcdate()),
[IsDemoAccount] [bit] NOT NULL CONSTRAINT [DF_PropertyInformations_IsDemoAccount] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[PropertyInformations] ADD CONSTRAINT [PK__Property__3214EC06F99D05AB] PRIMARY KEY NONCLUSTERED  ([Id])
GO
