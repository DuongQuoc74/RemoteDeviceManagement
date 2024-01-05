CREATE PROCEDURE pi_PromoteUserRols
	@Id int
AS
BEGIN    
	UPDATE CT_UsersRols
	SET
		FKRolID = 2
	WHERE PKRegisterID = @Id
END