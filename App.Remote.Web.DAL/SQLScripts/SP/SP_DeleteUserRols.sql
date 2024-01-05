CREATE PROCEDURE pi_DeleteUserRols
	@Id int
AS
BEGIN    
	DELETE FROM CT_UsersRols
	WHERE PKRegisterID = @Id
END