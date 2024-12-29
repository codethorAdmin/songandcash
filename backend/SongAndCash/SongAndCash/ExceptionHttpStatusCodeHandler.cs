using SongAndCash.Exceptions;

namespace SongAndCash;

public static class ExceptionHttpStatusCodeHandler
{
    public static int FromException(Exception? exception)
    {
        return exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            EntityNotFoundException or EntityValidationException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}