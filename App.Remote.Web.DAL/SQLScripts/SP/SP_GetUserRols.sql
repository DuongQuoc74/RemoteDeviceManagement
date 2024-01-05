CREATE PROCEDURE pi_GetUserRols
AS
BEGIN
	SELECT t1.PKRegisterID as Id, t1.EmployeeNumber, t1.WindowsNT, t1.FKRolID as RoleId, t1.Available
	From CT_UsersRols t1
	WHERE t1.Available = 1
END