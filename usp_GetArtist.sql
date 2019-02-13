ALTER PROCEDURE usp_GetArtist
(
@pNombre NVARCHAR(100)
)
as
Begin
Select ArtistId,Name from Artist
where Name like @pNombre
end
