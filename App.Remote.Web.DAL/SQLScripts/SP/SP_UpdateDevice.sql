CREATE PROCEDURE pi_UpdateDevice
	@Id int,
	@Remark nvarchar(1000),
	@UpdatedBy nvarchar(50),
	@Updated datetime
AS
BEGIN    
	UPDATE PI_Devices
	SET
		Remark = @Remark,
		UpdatedBy = @UpdatedBy,
		Updated = @Updated
	WHERE PKPicoID = @Id;
END