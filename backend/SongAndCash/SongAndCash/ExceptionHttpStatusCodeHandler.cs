using SongAndCash.Exceptions;

namespace SongAndCash;

public static class ExceptionHttpStatusCodeHandler
{
    public static int FromException(Exception? exception)
    {
        return exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            EntityValidationException => StatusCodes.Status422UnprocessableEntity,
            EntityNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}
