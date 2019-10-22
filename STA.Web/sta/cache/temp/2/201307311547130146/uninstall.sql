IF OBJECT_ID('plus_advisory') IS NOT NULL
DROP TABLE [plus_advisory]
GO
DELETE FROM [@tbprefix_variables] WHERE [key] = 'advisory_msgtype'
GO
DELETE FROM [@tbprefix_variables] WHERE [key] =  N'advisory_emails'