CREATE TRIGGER trg_INSERT_UPDATE_TripExpenseTypes
ON TripExpenseTypes  
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Name IS NULL OR LTRIM(RTRIM(Name)) = ''
    )
    BEGIN
        RAISERROR ('Имя не может быть пустым.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Standard <= 0
    )
    BEGIN
        RAISERROR ('Норма должна быть больше 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;