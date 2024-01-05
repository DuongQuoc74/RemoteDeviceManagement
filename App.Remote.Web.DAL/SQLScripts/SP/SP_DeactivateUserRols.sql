CREATE PROCEDURE pi_DeactivateUserRols
	@Id int
AS
BEGIN    
	UPDATE CT_UsersRols
	SET
		Available = 0
	WHERE PKRegisterID = @Id
END