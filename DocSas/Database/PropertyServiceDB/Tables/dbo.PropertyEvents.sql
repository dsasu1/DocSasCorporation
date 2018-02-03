CREATE TABLE [dbo].[PropertyEvents]
(
[Id] [uniqueidentifier] NOT NULL,
[UserId] [uniqueidentifier] NOT NULL,
[EventName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Details] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[StartDate] [datetime] NOT NULL,
[EndDate] [datetime] NULL,
[StartTime] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[EndTime] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StartTimeType] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EndTimeType] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsAllDayEvent] [bit] NOT NULL,
[ShareWithEnum] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RsvpMaybe] [int] NOT NULL,
[RsvpYes] [int] NOT NULL,
[RsvpNo] [int] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyE__Modif__74CE504D] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__PropertyE__Added__75C27486] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[PropertyEvents] ADD CONSTRAINT [PK__Property__3214EC069EEAE343] PRIMARY KEY NONCLUSTERED  ([Id])
GO
