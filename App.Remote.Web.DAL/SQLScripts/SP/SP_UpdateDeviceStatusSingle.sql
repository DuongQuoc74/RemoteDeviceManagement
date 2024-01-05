CREATE PROCEDURE pi_UpdateDevicesStatusSingle
	@Id int,
	@Status nvarchar(50),
	@UpdatedBy nvarchar(50),
	@Updated datetime,
	@Remark nvarchar(100),
	@MachineId int
AS
BEGIN    
	UPDATE PI_Devices
	SET
		Status = @Status,
		UpdatedBy = @UpdatedBy,
		Updated = @Updated
	WHERE PKPicoID = @Id;

	INSERT INTO PI_DeviceHistories(FKPicoID, Status, Created, CreatedBy, Remark, FKMachineID)
	VALUES (@Id, @Status, @Updated, @UpdatedBy, @Remark, @MachineId);
END