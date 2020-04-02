CREATE PROCEDURE PromoteStudents @Studies NVARCHAR(100), @SEMESTER INT
AS
BEGIN
	SET XACT_ABORT ON;
	BEGIN TRAN
	
	DECLARE @IdStudies INT = (SELECT IdStudy FROM Studies WHERE Name = @Studies);
	IF @idStudies IS NULL
	BEGIN
		RETURN;
	END

	DECLARE @IdEnrollment INT = (SELECT IdEnrollment FROM Enrollment WHERE IdStudy = @IdStudies AND Semester = @SEMESTER+1)
	IF @IdEnrollment IS NULL
	BEGIN
		INSERT INTO Enrollment(IdEnrollment, Semester, IdStudy, StartDate)
		VALUES((SELECT MAX(IdEnrollment)+1 FROM Enrollment), @SEMESTER+1, @IdStudies, GETDATE());
	END;

	DECLARE @OldIdEnrollment INT = (SELECT IdEnrollment FROM Enrollment WHERE IdStudy = @IdStudies AND Semester = @SEMESTER)

	UPDATE Enrollment
	SET Semester = @SEMESTER+1
	WHERE IdEnrollment = @OldIdEnrollment;
	COMMIT TRAN;

END;