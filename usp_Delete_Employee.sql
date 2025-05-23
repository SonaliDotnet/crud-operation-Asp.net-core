USE [CRUDwithADONET]
GO
/****** Object:  StoredProcedure [dbo].[usp_Delete_Employee]    Script Date: 4/13/2025 11:24:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[usp_Delete_Employee]
(
  @Id  INT
)
AS
BEGIN
Declare @RowCount INT = 0
  BEGIN TRY
    SET @RowCount = (SELECT COUNT(1) FROM DBO.Employees WITH (NOLOCK) WHERE Id=@Id)

	IF(@RowCount > 0)
	  BEGIN
	    BEGIN TRAN
	      DELETE FROM DBO.Employees
			WHERE Id = @Id
		  COMMIT TRAN
		END
	END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH
END
