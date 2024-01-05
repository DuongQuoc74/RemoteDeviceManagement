CREATE PROCEDURE pi_GetUnassignedMachines
AS
BEGIN
    SELECT t1.PKMachineID as Id, t1.MachineName as Name, t1.Available
    FROM CT_Machines t1
    LEFT JOIN PI_Devices t2 ON t2.FKMachineID = t1.PKMachineID
    WHERE t2.FKMachineID IS NULL AND t1.Available = 1
	ORDER BY t1.PKMachineID
END