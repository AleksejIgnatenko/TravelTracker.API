CREATE TRIGGER trg_INSERT_UPDATE_Cities
ON Cities
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Country IS NULL OR LTRIM(RTRIM(Country)) = ''
    )
    BEGIN
        RAISERROR ('Страна не может быть пустой.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Name IS NULL OR LTRIM(RTRIM(Name)) = ''
    )
    BEGIN
        RAISERROR ('Имя города не может быть пустым.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    INSERT INTO Cities (Country, Name)
    SELECT Country, Name
    FROM inserted;
END;