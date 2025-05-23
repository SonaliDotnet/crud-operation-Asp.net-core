USE [CRUDwithADONET]
GO
/****** Object:  StoredProcedure [dbo].[usp_Get_EmployeesById]    Script Date: 4/13/2025 11:22:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_Get_EmployeesById]
(
  @Id INT
)
AS
BEGIN
   SELECT Id,FirstName,LastName,DateOfBirth,Email,Salary From DBO.Employees WITH (NOLOCK)
    WHERE Id = @Id;
END