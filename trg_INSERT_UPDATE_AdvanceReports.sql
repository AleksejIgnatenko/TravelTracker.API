CREATE TRIGGER trg_INSERT_UPDATE_AdvanceReports
ON AdvanceReports
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE DateOfDelivery IS NOT NULL 
          AND NOT DateOfDelivery LIKE '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'
    )
    BEGIN
        RAISERROR ('Дата сдачи авансового отчета должна быть в формате yyyy-MM-dd.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;