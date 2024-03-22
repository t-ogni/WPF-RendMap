CREATE TABLE [dbo].[users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [login] NVARCHAR(50) NOT NULL, 
    [password] NVARCHAR(50) NOT NULL
)
