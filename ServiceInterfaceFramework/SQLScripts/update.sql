IF NOT EXISTS (SELECT * FROM sys.change_tracking_databases WHERE database_id = DB_ID(N'MvcWms')) 
   ALTER DATABASE [MvcWms] 
   SET  CHANGE_TRACKING = ON
GO
