USE [CRUDwithADONET]
GO
/****** Object:  StoredProcedure [dbo].[usp_Insert_Employee]    Script Date: 4/13/2025 11:23:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_Insert_Employee]
(
  @FirstName  NVARCHAR(250)
 ,@LastName   NVARCHAR(250)
 ,@DateOfBirth  DATE
  ,@Email       NVARCHAR(50)
  ,@Salary      FLOAT
)
AS
BEGIN

BEGIN TRY
  BEGIN TRAN
    INSERT INTO DBO.Employees(FirstName,LastName,DateOfBirth,Email,Salary)
	  VALUES
	  (
	    @FirstName  
       ,@LastName   
       ,@DateOfBirth  
       ,@Email      
       ,@Salary  
	  )
  COMMIT TRAN
  END TRY
  BEGIN CATCH
   ROLLBACK TRAN
   END CATCH
END
