CREATE TRIGGER trg_INSERT_UPDATE_TripCertificates
ON Commands 
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Title IS NULL OR LTRIM(RTRIM(Title)) = ''
    )
    BEGIN
        RAISERROR ('Заголовок не может быть пустой.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Description IS NULL OR LTRIM(RTRIM(Description)) = ''
    )
    BEGIN
        RAISERROR ('Описание не может быть пустым.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE DateIssued IS NULL OR LTRIM(RTRIM(DateIssued)) = ''
    )
    BEGIN
        RAISERROR ('Дата издания не может быть пустой.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;