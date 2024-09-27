ALTER TABLE [dbo].[EventUsers]
	ADD CONSTRAINT [FK_EventUsers_EventId]
	FOREIGN KEY ([EventId])
	REFERENCES [dbo].[Events] ([Id])
	ON DELETE CASCADE
