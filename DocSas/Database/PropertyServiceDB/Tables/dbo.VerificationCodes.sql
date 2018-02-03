CREATE TABLE [dbo].[VerificationCodes]
(
[Id] [uniqueidentifier] NOT NULL,
[VerifyCode] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserId] [uniqueidentifier] NOT NULL,
[VerifyType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MediaType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsVerified] [bit] NOT NULL,
[IsExpired] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Verificat__Modif__058EC7FB] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Verificat__Added__0682EC34] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[VerificationCodes] ADD CONSTRAINT [PK__Verifica__3214EC067C77EE96] PRIMARY KEY NONCLUSTERED  ([Id])
GO
