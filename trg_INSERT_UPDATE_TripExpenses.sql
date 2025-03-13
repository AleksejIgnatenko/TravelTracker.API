CREATE TRIGGER trg_INSERT_UPDATE_TripExpenses
ON TripExpenses  
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Amount <= 0
    )
    BEGIN
        RAISERROR ('Сумма не должна быть меньше или равна 0.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Date NOT LIKE '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'
    )
    BEGIN
        RAISERROR ('Дата должна быть в формате yyyy-MM-dd.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;