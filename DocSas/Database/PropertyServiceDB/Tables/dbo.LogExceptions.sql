CREATE TABLE [dbo].[LogExceptions]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NULL,
[ExceptionMessage] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [timestamp] NOT NULL,
[IsResolved] [bit] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__LogExcept__Modif__2EA5EC27] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__LogExcept__Added__2F9A1060] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[LogExceptions] ADD CONSTRAINT [PK__LogExcep__3214EC06FA5FCF6A] PRIMARY KEY NONCLUSTERED  ([Id])
GO
