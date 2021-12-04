CREATE TABLE [dbo].[ParameterXRefMultiSelect] (
    [XRefID]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ParameterID] UNIQUEIDENTIFIER NOT NULL,
    [Value]       VARCHAR (50)     NOT NULL,
    PRIMARY KEY CLUSTERED ([XRefID] ASC),
    CONSTRAINT [FK_ParameterXRefMultiSelectValues_Parameters] FOREIGN KEY ([ParameterID]) REFERENCES [dbo].[Parameters] ([ParameterID])
);

