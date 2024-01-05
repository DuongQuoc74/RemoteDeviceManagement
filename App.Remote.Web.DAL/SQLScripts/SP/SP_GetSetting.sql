CREATE PROCEDURE [dbo].[pi_GetSettings]
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT* From PI_Settings st
	WHERE Available = 1
END