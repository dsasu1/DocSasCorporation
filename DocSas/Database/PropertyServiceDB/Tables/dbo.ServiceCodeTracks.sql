CREATE TABLE [dbo].[ServiceCodeTracks]
(
[Id] [uniqueidentifier] NOT NULL,
[CodeType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StartValue] [bigint] NOT NULL,
[MaxInd] [bigint] NOT NULL,
[AvailableLetters] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LettersLength] [int] NOT NULL,
[NumberPosition] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ServiceCo__Modif__7F80E8EA] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__ServiceCo__Added__00750D23] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[ServiceCodeTracks] ADD CONSTRAINT [PK__ServiceC__3214EC06793839F7] PRIMARY KEY NONCLUSTERED  ([Id])
GO
