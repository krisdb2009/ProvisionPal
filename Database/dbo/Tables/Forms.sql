CREATE TABLE [dbo].[Forms] (
    [FormID]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]        VARCHAR (50)     DEFAULT ('') NOT NULL,
    [Description] TEXT             NULL,
    PRIMARY KEY CLUSTERED ([FormID] ASC)
);

