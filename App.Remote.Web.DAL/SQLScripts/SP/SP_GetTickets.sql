CREATE PROCEDURE pi_GetTickets
    @StartDate datetime,
    @EndDate datetime,
    @MachineName nvarchar(50)
AS
BEGIN
    IF @StartDate IS NULL OR @EndDate IS NULL
    BEGIN
        IF (@machineName IS NULL)
        BEGIN
            SELECT TOP 50 t1.PKTiketID as Id, t1.FKMachineID as MachineId, t2.MachineName as MachineName, t1.FKClasificationID as ClassificationId, t1.DateStop, t1.DateRun, t1.NewStartTime, t1.ClosedBY as ClosedBy, t1.Level, t1.PartNumber, t1.Description
            FROM CT_Tikets t1
            LEFT JOIN CT_Machines t2 ON t2.PKMachineID = t1.FKMachineID
            ORDER BY t1.DateStop DESC
        END
        ELSE
        BEGIN
            SELECT TOP 50 t1.PKTiketID as Id, t1.FKMachineID as MachineId, t2.MachineName as MachineName, t1.FKClasificationID as ClassificationId, t1.DateStop, t1.DateRun, t1.NewStartTime, t1.ClosedBY as ClosedBy, t1.Level, t1.PartNumber, t1.Description as DeviceGuid
            FROM CT_Tikets t1
            INNER JOIN CT_Machines t2 ON t2.PKMachineID = t1.FKMachineID
            WHERE t2.MachineName = @machineName
            ORDER BY t1.DateStop DESC
        END
    END
    ELSE
    BEGIN
        IF (@machineName IS NULL)
        BEGIN
            SELECT TOP 50 t1.PKTiketID as Id, t1.FKMachineID as MachineId, t2.MachineName as MachineName, t1.FKClasificationID as ClassificationId, t1.DateStop, t1.DateRun, t1.NewStartTime, t1.ClosedBY as ClosedBy, t1.Level, t1.PartNumber, t1.Description
            FROM CT_Tikets t1
            LEFT JOIN CT_Machines t2 ON t2.PKMachineID = t1.FKMachineID
            WHERE t1.DateStop >= @StartDate AND t1.DateStop <= @EndDate
            ORDER BY t1.DateStop DESC
        END
        ELSE
        BEGIN
            SELECT TOP 50 t1.PKTiketID as Id, t1.FKMachineID as MachineId, t2.MachineName as MachineName, t1.FKClasificationID as ClassificationId, t1.DateStop, t1.DateRun, t1.NewStartTime, t1.ClosedBY as ClosedBy, t1.Level, t1.PartNumber, t1.Description
            FROM CT_Tikets t1
            INNER JOIN CT_Machines t2 ON t2.PKMachineID = t1.FKMachineID
            WHERE t1.DateStop >= @StartDate AND t1.DateStop <= @EndDate AND t2.MachineName = @machineName
            ORDER BY t1.DateStop DESC
        END    
    END
END