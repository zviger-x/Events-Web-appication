CREATE TABLE [dbo].[Events]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(120) NOT NULL UNIQUE, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    [Venue] NVARCHAR(120) NOT NULL,
    [Category] NVARCHAR(120) NOT NULL, 
    [MaxNumberOfParticipants] INT NOT NULL, 
    [Image] IMAGE NULL
)
