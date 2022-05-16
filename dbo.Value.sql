CREATE TABLE [dbo].[Value] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [ValQue] NVARCHAR (MAX) NULL,
    [ValAns] NVARCHAR (MAX) NULL,
    [Email] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Value] PRIMARY KEY CLUSTERED ([Id] ASC)
);


