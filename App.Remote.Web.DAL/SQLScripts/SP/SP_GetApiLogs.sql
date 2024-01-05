CREATE PROCEDURE pi_GetApiLogs
	@StartDate datetime,
    @EndDate datetime,
	@GuidDevice uniqueidentifier
AS
BEGIN
	IF @StartDate IS NULL OR @EndDate IS NULL
	BEGIN
		IF(@GuidDevice IS NULL)
		BEGIN
			SELECT TOP 50 PKApiLogsID as Id, Api as Api, Request as Request, Response as Response, FKMachineId as MachineId, Created as Created, CreatedBy as CreatedBy, DeviceGuid as DeviceGuid
			FROM PI_ApiLogs
			ORDER BY Created DESC
		END
		ELSE
		BEGIN
			SELECT TOP 50 PKApiLogsID as Id, Api as Api, Request as Request, Response as Response, FKMachineId as MachineId, Created as Created, CreatedBy as CreatedBy, DeviceGuid as DeviceGuid
			FROM PI_ApiLogs
			WHERE DeviceGuid = @GuidDevice
			ORDER BY Created DESC
		END
	END
	ELSE
	BEGIN
		IF(@GuidDevice IS NULL)
		BEGIN
			SELECT TOP 50 PKApiLogsID as Id, Api as Api, Request as Request, Response as Response, FKMachineId as MachineId, Created as Created, CreatedBy as CreatedBy, DeviceGuid as DeviceGuid
			FROM PI_ApiLogs
			WHERE Created >= @StartDate AND Created <= @EndDate
			ORDER BY Created DESC
		END
		ELSE
		BEGIN
			SELECT TOP 50 PKApiLogsID as Id, Api as Api, Request as Request, Response as Response, FKMachineId as MachineId, Created as Created, CreatedBy as CreatedBy, DeviceGuid as DeviceGuid
			FROM PI_ApiLogs
			WHERE Created >= @StartDate AND Created <= @EndDate AND DeviceGuid = @GuidDevice
			ORDER BY Created DESC
		END	
	END
END