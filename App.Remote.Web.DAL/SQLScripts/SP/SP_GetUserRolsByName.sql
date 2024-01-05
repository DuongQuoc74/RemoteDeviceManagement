CREATE PROCEDURE pi_GetUserRolsByName
	@UserName nvarchar(50)
AS
BEGIN
	SELECT t1.PKRegisterID as Id, t1.EmployeeNumber, t1.WindowsNT, t1.FKRolID as RoleId, t1.Available
	From CT_UsersRols t1
	WHERE t1.Available = 1 AND (t1.EmployeeNumber = @UserName OR t1.WindowsNT = @UserName)
END