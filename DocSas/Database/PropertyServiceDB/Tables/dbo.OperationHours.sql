CREATE TABLE [dbo].[OperationHours]
(
[Id] [uniqueidentifier] NOT NULL,
[PropertyInformationId] [uniqueidentifier] NOT NULL,
[DayKey] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[OpenTime] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[OpenTimeType] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CloseTime] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CloseTimeType] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsClosed] [bit] NOT NULL,
[Version] [timestamp] NOT NULL,
[ModifiedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Operation__Modif__3FF073BA] DEFAULT (getutcdate()),
[AddedDateUtc] [datetime] NOT NULL CONSTRAINT [DF__Operation__Added__40E497F3] DEFAULT (getutcdate())
)
GO
ALTER TABLE [dbo].[OperationHours] ADD CONSTRAINT [PK__Operatio__3214EC0633F5F562] PRIMARY KEY NONCLUSTERED  ([Id])
GO
