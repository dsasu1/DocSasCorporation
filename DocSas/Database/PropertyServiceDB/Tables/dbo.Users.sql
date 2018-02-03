CREATE TABLE [dbo].[Users]
(
[Id] [uniqueidentifier] NOT NULL,
[FirstName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserName] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Lang] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Password] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SecurityQuestionId] [uniqueidentifier] NULL,
[UserSecurityAns] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ServiceCode] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhotoOriginal] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhotoThumbnail] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserTypeEnum] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UserSalt] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastLogin] [datetime] NULL,
[TimeZone] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsAcceptedTerms] [bit] NOT NULL,
[IsActive] [bit] NULL,
[IsBanned] [bit] NOT NULL,
[LoginAttempts] [int] NOT NULL,
[IsLockedOut] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[IPAddress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Users__ModifiedD__00CA12DE] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Users__AddedDate__01BE3717] DEFAULT (getutcdate()),
[LoginSessionId] [uniqueidentifier] NULL,
[IsDemoAccount] [bit] NOT NULL CONSTRAINT [DF_Users_IsDemoAccount] DEFAULT ((0))
)
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__3214EC069BB04DB9] PRIMARY KEY NONCLUSTERED  ([Id])
GO
