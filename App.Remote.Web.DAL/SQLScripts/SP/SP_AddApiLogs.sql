CREATE PROCEDURE pi_AddApiLogs
	@Api nvarchar(50),
	@Request nvarchar(1000),
	@Response nvarchar(max),
	@MachineId int,
	@Created datetime,
	@CreatedBy nvarchar(100),
	@DeviceGuid uniqueidentifier
AS
BEGIN    
	INSERT INTO PI_ApiLogs(Api, Request, Response, FKMachineId, Created, CreatedBy, DeviceGuid)
	VALUES (@Api, @Request, @Response, @MachineId, @Created, @CreatedBy, @DeviceGuid);
END