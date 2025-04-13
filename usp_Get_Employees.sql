USE [CRUDwithADONET]
GO
/****** Object:  StoredProcedure [dbo].[usp_Get_Employees]    Script Date: 4/13/2025 11:24:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_Get_Employees]
As
BEGIN
    SELECT Id, FirstName, LastName, DateOfBirth, Email, Salary From DBO.Employees WITH (NOLOCK)
END