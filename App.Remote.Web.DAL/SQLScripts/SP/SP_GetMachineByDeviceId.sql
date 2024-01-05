CREATE PROCEDURE pi_GetMachineByDeviceId
		@DeviceId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT m.PKMachineID as Id, m.MachineName as Name
	FROM PI_Devices d
	JOIN CT_Machines m ON d.FKMachineID = m.PKMachineID
	WHERE d.PKPicoID = @DeviceId
END