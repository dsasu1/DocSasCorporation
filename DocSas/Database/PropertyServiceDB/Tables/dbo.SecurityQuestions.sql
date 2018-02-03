CREATE TABLE [dbo].[SecurityQuestions]
(
[Id] [uniqueidentifier] NOT NULL,
[Question] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsValid] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL,
[AddedDateUtc] [datetime] NOT NULL
)
GO
ALTER TABLE [dbo].[SecurityQuestions] ADD CONSTRAINT [PK__Security__3214EC06BEEE03E9] PRIMARY KEY NONCLUSTERED  ([Id])
GO
