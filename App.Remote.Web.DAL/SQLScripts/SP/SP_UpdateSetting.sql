CREATE PROCEDURE pi_UpdateSetting
	@Name nvarchar(50),
	@Value nvarchar(1000),
	@Available bit
AS
BEGIN    
	UPDATE PI_Settings
	SET
		Value = @Value,
		Available = @Available
	WHERE Name = @Name;
END