ALTER TABLE [dbo].[EventUsers]
	ADD CONSTRAINT [FK_EventUsers_UserId]
	FOREIGN KEY ([UserId])
	REFERENCES [dbo].[Users] ([Id])
	ON DELETE CASCADE