CREATE TABLE [dbo].[Logs] (
    [LogID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Time]     DATETIME         DEFAULT (sysdatetime()) NOT NULL,
    [Level]    TINYINT          DEFAULT ((255)) NOT NULL,
    [Identity] VARCHAR (50)     NOT NULL,
    [Source]   VARCHAR (50)     NOT NULL,
    [Message]  VARCHAR (MAX)    NOT NULL,
    PRIMARY KEY CLUSTERED ([LogID] ASC)
);



