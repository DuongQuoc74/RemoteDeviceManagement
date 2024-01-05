CREATE PROCEDURE pi_GetSettingByName
    @name NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM PI_Settings
    WHERE LOWER(Name) = LOWER(@name) AND Available = 1
END