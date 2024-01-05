CREATE PROCEDURE pi_AddDevicesStatusHistories
	@Id int,
	@Status nvarchar(50),
	@CreatedBy nvarchar(50),
	@Created datetime,
	@Remark nvarchar(100),
	@MachineId int
AS
BEGIN    
	INSERT INTO PI_DeviceHistories(FKPicoID, Status, Created, CreatedBy, Remark, FKMachineID)
	VALUES (@Id, @Status, @Created, @CreatedBy, @Remark, @MachineId);
END