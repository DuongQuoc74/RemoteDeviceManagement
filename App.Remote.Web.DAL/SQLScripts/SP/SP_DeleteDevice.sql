CREATE PROCEDURE pi_DeleteDevice
	@DeviceId int,
	@Status nvarchar(50),
	@CreatedBy nvarchar(50),
	@Created datetime,
	@Remark nvarchar(100)
AS
BEGIN
	DECLARE @MachineId int;
	SELECT @MachineId = t1.FKMachineID
	FROM PI_Devices t1
	WHERE PKPicoID = @DeviceId

	DELETE FROM PI_Devices
	WHERE PKPicoID = @DeviceId;

	INSERT INTO PI_DeviceHistories(FKPicoID, Status, Created, CreatedBy, Remark, FKMachineID)
	VALUES (@DeviceId, @Status, @Created, @CreatedBy, @Remark, @MachineId);
END