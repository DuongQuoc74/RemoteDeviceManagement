CREATE PROCEDURE pi_UpdateDevicesStatus
@IdsXml xml,
@Status nvarchar(50),
@UpdatedBy nvarchar(50),
@Updated datetime
AS
BEGIN    
	UPDATE PI_Devices
	SET
		Status = @Status
		,UpdatedBy = @UpdatedBy
		,Updated = @Updated
	WHERE PKPicoID IN (SELECT T.c.value('@Id', 'int') FROM @IdsXml.nodes('/Ids/Id') AS T(c));
END