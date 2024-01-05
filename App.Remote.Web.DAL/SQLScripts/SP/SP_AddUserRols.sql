CREATE PROCEDURE [dbo].[pi_AddUserRols]
	@EmployeeNumber nvarchar(50),
	@WindowsNT nvarchar(50),
	@RolID int,
	@Available bit
AS
BEGIN    
	IF NOT EXISTS (
		SELECT 1
		FROM CT_UsersRols
		WHERE EmployeeNumber = @EmployeeNumber
	)
	BEGIN
		INSERT INTO CT_UsersRols (EmployeeNumber, WindowsNT, FKRolID, Available)
		VALUES (@EmployeeNumber, @WindowsNT, @RolID, @Available)
	END
END