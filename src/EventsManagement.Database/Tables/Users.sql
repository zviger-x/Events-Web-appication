CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Surname] NCHAR(50) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL
)
