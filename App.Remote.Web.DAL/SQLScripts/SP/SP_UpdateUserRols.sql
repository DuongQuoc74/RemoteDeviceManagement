CREATE PROCEDURE pi_UpdateUserRols
	@EmployeeNumber nvarchar(50),
	@WindowsNT nvarchar(50),
	@RolID int,
	@Available bit,
	@Id int
AS
BEGIN    
	UPDATE CT_UsersRols
	SET
		EmployeeNumber = @EmployeeNumber,
		WindowsNT = @WindowsNT,
		FKRolID = @RolID,
		Available = @Available
	WHERE PKRegisterID = @Id
END