USE [QLBanHang]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginCheck]    Script Date: 07/03/2022 3:52:05 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[sp_LoginCheck]
		@user varchar(20),
		@password varchar(20)
AS
BEGIN
	
	Select * from Users where Users.username = @user and Users.passwords = @password

END
go 