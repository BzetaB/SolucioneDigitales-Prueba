use [BD_Matriculas]
go

CREATE PROCEDURE sp_FindMatriculaByEstudianteIdAndCursoId
    @EstudianteId BIGINT,
    @CursoId BIGINT
AS
BEGIN
    SELECT TOP 1 *
    FROM Matriculas
    WHERE EstudianteId = @EstudianteId AND CursoId = @CursoId;
END
GO

CREATE PROCEDURE sp_UpdateMatriculaEstado
    @MatriculaId BIGINT,
    @NuevoEstado VARCHAR(20),
    @ResultadoMensaje NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EstadoActual VARCHAR(20);

    -- Verificar si la matrícula existe
    IF NOT EXISTS (SELECT 1 FROM matriculas WHERE MatriculaId = @MatriculaId)
    BEGIN
        SET @ResultadoMensaje = 'No se encontró la matrícula.';
        RETURN;
    END

    -- Validar que el nuevo estado es válido
    IF @NuevoEstado NOT IN ('ACTIVA', 'CANCELADA', 'FINALIZADA')
    BEGIN
        SET @ResultadoMensaje = 'El estado ingresado no es válido.';
        RETURN;
    END

    -- Obtener estado actual
    SELECT @EstadoActual = Status FROM matriculas WHERE MatriculaId = @MatriculaId;

    -- Validar cambio de estado
    IF @EstadoActual = 'FINALIZADA' AND (@NuevoEstado = 'CANCELADA' OR @NuevoEstado = 'ACTIVA')
    BEGIN
        SET @ResultadoMensaje = 'No se puede cambiar el estado de FINALIZADA a CANCELADA o ACTIVA.';
        RETURN;
    END

    -- Realizar la actualización
    UPDATE matriculas SET Status = @NuevoEstado WHERE MatriculaId = @MatriculaId;

    SET @ResultadoMensaje = 'Actualización exitosa.';
END