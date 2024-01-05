CREATE PROCEDURE pi_GetDeviceDetail
@DeviceId int
AS
BEGIN    
	SELECT t1.PKPicoID as Id, t1.FKMachineID as MachineId, t4.MachineName as MachineName, t1.Guid, t1.Status, t1.Remark, t1.Created, t1.CreatedBy, t1.Updated, t1.UpdatedBy, t2.PKTiketID as TicketId, t2.DateStop as TicketDateStop, t2.ClosedBY as TicketClosedBy, t2.PartNumber as TicketPartNumber, t3.Api, t3.Created as ApiCreated, t3.CreatedBy as ApiCreatedby, t5.LineName as LineName, t5.PKLineID as LineId, t3.Created as LastOnlineCheck
	FROM PI_Devices t1
	LEFT JOIN (
	  SELECT *, ROW_NUMBER() OVER (PARTITION BY FKMachineID ORDER BY DateStop DESC) AS rn
	  FROM CT_Tikets
	) t2 ON t2.FKMachineID = t1.FKMachineID AND t2.rn = 1
	LEFT JOIN (
	  SELECT *, ROW_NUMBER() OVER (PARTITION BY DeviceGuid ORDER BY Created DESC) AS rn
	  FROM PI_ApiLogs
	) t3 ON t3.DeviceGuid = t1.Guid AND t3.rn = 1
	LEFT JOIN (
		SELECT *
		FROM CT_Machines
	) t4 ON t4.PKMachineID = t1.FKMachineID
	LEFT JOIN (
		SELECT *
		FROM CT_Lines
	) t5 ON t5.PKLineID=t4.FKLineID
	WHERE t1.PKPicoID = @DeviceId
END