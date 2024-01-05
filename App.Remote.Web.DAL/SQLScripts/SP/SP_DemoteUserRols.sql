CREATE PROCEDURE pi_DemoteUserRols
	@Id int
AS
BEGIN    
	UPDATE CT_UsersRols
	SET
		FKRolID = 1
	WHERE PKRegisterID = @Id
END