USE [CRUDwithADONET]
GO
/****** Object:  StoredProcedure [dbo].[usp_Update_Employee]    Script Date: 4/13/2025 11:23:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_Update_Employee]
(
  @Id        INT
 ,@FirstName  NVARCHAR(250)
 ,@LastName   NVARCHAR(250)
 ,@DateOfBirth  DATE
  ,@Email       NVARCHAR(250)
  ,@Salary      FLOAT
)
AS
BEGIN
Declare @RowCount INT = 0
  BEGIN TRY
    SET @RowCount = (SELECT COUNT(1) FROM DBO.Employees WITH (NOLOCK) WHERE Id=@Id)

	IF(@RowCount > 0)
	  BEGIN
	    BEGIN TRAN
	      UPDATE DBO.Employees
		     SET
			    FirstName = @FirstName
				,LastName =  @LastName
				,DateOfBirth = @DateOfBirth
				,Email       = @Email
				,Salary      = @Salary
			WHERE Id = @Id
		  COMMIT TRAN
		END
	END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH
END