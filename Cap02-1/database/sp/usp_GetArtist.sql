CREATE PROCEDURE usp_GetArtist
(
	@pNombre NVARCHAR(100)
)
AS
BEGIN

	SELECT ArtistId,Name 
	FROM Artist
	WHERE Name like @pNombre

END

GO

CREATE PROCEDURE usp_InsertArtist
(
@Name NVARCHAR(120)
)
AS
BEGIN
	
	INSERT INTO Artist(Name)
	VALUES(@Name)

	SELECT SCOPE_IDENTITY()

END

go

CREATE PROCEDURE usp_InsertArtistWithOutput
(
@Name NVARCHAR(120),
@ID INT OUTPUT
)
AS
BEGIN
	
	INSERT INTO Artist(Name)
	VALUES(@Name)

	SET @ID = SCOPE_IDENTITY()

END

