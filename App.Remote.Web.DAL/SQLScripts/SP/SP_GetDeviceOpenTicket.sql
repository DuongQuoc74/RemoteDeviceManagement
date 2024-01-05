CREATE PROCEDURE pi_GetDeviceOpenTicket
	@MachineGuid uniqueidentifier,
	@Guid uniqueidentifier
AS
BEGIN 
	DECLARE @TicketsValidDays int;
	SELECT @TicketsValidDays = CAST(st.Value AS int)
	FROM PI_Settings st	
	WHERE st.Name = 'TicketsValidDays';

	DECLARE @TicketsValidDaysToCompare datetime;
	SET @TicketsValidDaysToCompare = DATEADD(DAY, -@TicketsValidDays, GETDATE());

	DECLARE @TicketsApiCheckTimeOutMinutes int;
	SELECT @TicketsApiCheckTimeOutMinutes = CAST(st.Value AS int)
	FROM PI_Settings st	
	WHERE st.Name = 'TicketsApiCheckTimeOutMinutes';

	DECLARE @TicketsApiCheckTimeOutMinutesToCompare datetime;
	SET @TicketsApiCheckTimeOutMinutesToCompare = DATEADD(MINUTE, -@TicketsApiCheckTimeOutMinutes, GETDATE());

	IF @Guid IS NOT NULL
		BEGIN
			SELECT TOP 1 t1.PKTiketID as Id, t1.FKMachineID as MachineId, t1.FKClasificationID as ClassificationId, t1.DateStop as DateStop, t1.DateStop as DateRun, t1.NewStartTime as NewStartTime, t1.Level as Level, t1.ClosedBY as ClosedBy, t1.PartNumber as PartNumber, t1.Description as Description
			FROM CT_Tikets t1
			INNER JOIN PI_Devices t2 ON t2.FKMachineID = t1.FKMachineID
			WHERE t2.Guid = @Guid AND t1.ClosedBY IS NULL AND t1.DateStop >= @TicketsValidDaysToCompare
			ORDER BY t1.DateStop DESC;
		END
	ELSE
		BEGIN
			SELECT TOP 1 t1.PKTiketID as Id, t1.FKMachineID as MachineId, t1.FKClasificationID as ClassificationId, t1.DateStop as DateStop, t1.DateStop as DateRun, t1.NewStartTime as NewStartTime, t1.Level as Level, t1.ClosedBY as ClosedBy, t1.PartNumber as PartNumber, t1.Description as Description
			FROM CT_Tikets t1
			INNER JOIN PI_Devices t2 ON t2.FKMachineID = t1.FKMachineID
			WHERE t2.Guid = @MachineGuid AND t1.ClosedBY IS NULL AND t1.DateStop >= @TicketsValidDaysToCompare AND t1.DateStop <= @TicketsApiCheckTimeOutMinutesToCompare AND t2.Status='active'
			ORDER BY t1.DateStop DESC;
		END
END