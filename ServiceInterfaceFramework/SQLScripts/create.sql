IF NOT EXISTS (SELECT * FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID(N'[dbo].[Service_Info]')) 
   ALTER TABLE [dbo].[Service_Info] 
   ENABLE  CHANGE_TRACKING
GO
