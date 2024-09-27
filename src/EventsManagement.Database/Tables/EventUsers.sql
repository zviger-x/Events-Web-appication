CREATE TABLE [dbo].[EventUsers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [EventId] INT NOT NULL, 
    [Registration] DATETIME NOT NULL 
)
