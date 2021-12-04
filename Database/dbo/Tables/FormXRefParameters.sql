CREATE TABLE [dbo].[FormXRefParameters] (
    [XRefID]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [FormID]      UNIQUEIDENTIFIER NOT NULL,
    [ParameterID] UNIQUEIDENTIFIER NOT NULL,
    [Order]       INT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([XRefID] ASC),
    CONSTRAINT [FK_FormXRefParameters_Form] FOREIGN KEY ([FormID]) REFERENCES [dbo].[Forms] ([FormID]),
    CONSTRAINT [FK_FormXRefParameters_Parameters] FOREIGN KEY ([ParameterID]) REFERENCES [dbo].[Parameters] ([ParameterID])
);

