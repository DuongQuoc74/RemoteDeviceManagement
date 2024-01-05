CREATE PROCEDURE pi_GetTicketByMachine
		@MachineId int
AS
BEGIN
	SELECT t1.PKTiketID as Id, t1.FKMachineID as MachineId, t2.MachineName as MachineName, t1.FKClasificationID as ClassificationId, t1.DateStop, t1.DateRun, t1.NewStartTime, t1.ClosedBY as ClosedBy, t1.Level, t1.PartNumber, t1.Description 
	FROM CT_Tikets t1
	INNER JOIN CT_Machines t2 ON t2.PKMachineID = t1.FKMachineID
	WHERE t1.FKMachineID = @MachineId
	ORDER BY t1.DateStop DESC
END