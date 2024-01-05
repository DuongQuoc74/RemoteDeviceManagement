CREATE PROCEDURE pi_GetDeviceByGuid
		@Guid uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT t1.PKPicoID as Id, t1.FKMachineID as MachineId, t1.Guid, t1.Status, t1.Remark, t1.Created, t1.CreatedBy, t1.Updated, t1.UpdatedBy, t1.Available From PI_Devices t1
	WHERE t1.Guid = @Guid;
END