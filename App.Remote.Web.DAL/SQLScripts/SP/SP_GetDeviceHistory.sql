CREATE PROCEDURE pi_GetDeviceHistories
		@DeviceId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT dh.PKDeviceHistoryID as Id, dh.FKPicoID as DeviceId,dh.FKMachineID as MachineId, dh.Created, dh.CreatedBy, dh.Remark, dh.Status From PI_DeviceHistories dh
	WHERE dh.FKPicoID = @DeviceId;

END