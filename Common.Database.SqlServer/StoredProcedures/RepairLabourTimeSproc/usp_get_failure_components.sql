CREATE PROCEDURE [dbo].[usp_get_failure_components]
	@pageNumber int,
	@pageSize int
AS
BEGIN
	SELECT * FROM failure_component f
	ORDER BY f.title
	OFFSET @pageSize * (@pageNumber - 1) ROWS  
	FETCH NEXT @pageSize ROWS ONLY
END