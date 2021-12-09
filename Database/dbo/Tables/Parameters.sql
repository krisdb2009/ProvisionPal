CREATE TABLE [dbo].[Parameters] (
    [ParameterID]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]              VARCHAR (50)     NOT NULL,
    [Description]       VARCHAR (300)    NULL,
    [RegularExpression] VARCHAR (MAX)    NULL,
    [Required]          BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ParameterID] ASC)
);



