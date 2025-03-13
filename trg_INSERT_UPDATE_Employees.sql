CREATE TRIGGER trg_INSERT_UPDATE_Employees
ON Employees 
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE FirstName IS NULL OR LTRIM(RTRIM(FirstName)) = ''
    )
    BEGIN
        RAISERROR ('Имя не может быть пустым.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE FirstName NOT LIKE '[а-яА-Яa-zA-Z]%'
    )
    BEGIN
        RAISERROR ('Имя должно содержать только буквы.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE LastName IS NULL OR LTRIM(RTRIM(LastName)) = ''
    )
    BEGIN
        RAISERROR ('Фамилия не может быть пустой.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE LastName NOT LIKE '[а-яА-Яa-zA-Z]% %'
    )
    BEGIN
        RAISERROR ('Фамилия должна содержать только буквы и допускать двойные фамилии.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE MiddleName IS NULL OR LTRIM(RTRIM(MiddleName)) = ''
    )
    BEGIN
        RAISERROR ('Отчество не может быть пустым.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE MiddleName NOT LIKE '[а-яА-Яa-zA-Z]%'
    )
    BEGIN
        RAISERROR ('Отчество должно содержать только буквы.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Position IS NULL OR LTRIM(RTRIM(Position)) = ''
    )
    BEGIN
        RAISERROR ('Должность не может быть пустой.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    IF EXISTS (
        SELECT * 
        FROM inserted 
        WHERE Department IS NULL OR LTRIM(RTRIM(Department)) = ''
    )
    BEGIN
        RAISERROR ('Отдел не может быть пустым.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;