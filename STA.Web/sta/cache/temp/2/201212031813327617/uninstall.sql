IF OBJECT_ID('plus_pickerr') IS NOT NULL
DROP TABLE [plus_pickerr]
GO
DELETE FROM [@tbprefix_variables] WHERE [key] = 'pickerr_type'