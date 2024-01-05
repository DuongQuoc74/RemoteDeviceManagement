CREATE PROCEDURE pi_AddDevice
	@MachineId int,
	@Remark nvarchar(1000),
	@CreatedBy nvarchar(50),
	@Created datetime,
	@Available bit,
	@Guid uniqueidentifier,
	@Status nvarchar(50),
	@RemarkHistory nvarchar(100),
	@InsertedId int OUTPUT
AS
BEGIN    
	IF NOT EXISTS (
		SELECT 1
		FROM PI_Devices
		WHERE FKMachineID = @MachineId
	)
	BEGIN
		INSERT INTO PI_Devices(FKMachineID, Remark, CreatedBy, Created, Available, Guid, Status)
		OUTPUT INSERTED.PKPicoID
		VALUES (@MachineId, @Remark, @CreatedBy, @Created, @Available, @Guid, @Status)
		SET @InsertedId = SCOPE_IDENTITY();
		INSERT INTO PI_DeviceHistories(FKPicoID, Status, Created, CreatedBy, Remark, FKMachineID)
		VALUES (@InsertedId, @Status, @Created, @CreatedBy, @RemarkHistory, @MachineId);
	END
END