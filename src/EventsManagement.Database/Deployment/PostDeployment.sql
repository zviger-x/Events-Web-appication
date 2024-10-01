-- Создание тестовых объектов
INSERT INTO [dbo].[Users]
VALUES
('TestName1', 'TestSurname1', GETDATE(), 'Test@Email.com'),
('TestName2', 'TestSurname2', GETDATE(), 'Test@Email.com')

INSERT INTO [dbo].[Events]
VALUES
('TestEvent', 'TestEventDescription', GETDATE(), 'TestVenue', 'TestCategory', 10, NULL)

INSERT INTO [dbo].[EventUsers]
VALUES
(1, 1, GETDATE()),
(2, 1, GETDATE())

DELETE FROM [dbo].[Users] WHERE [Id] = 2