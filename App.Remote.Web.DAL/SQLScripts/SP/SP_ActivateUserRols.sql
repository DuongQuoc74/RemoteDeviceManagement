CREATE PROCEDURE pi_ActivateUserRols
	@Id int
AS
BEGIN    
	UPDATE CT_UsersRols
	SET
		Available = 1
	WHERE PKRegisterID = @Id
END