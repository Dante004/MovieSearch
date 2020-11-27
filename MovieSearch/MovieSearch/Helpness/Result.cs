using System.Collections.Generic;

namespace MovieSearch.Helpness
{
    public class Result
    {
        public bool Success { get; set; }
        public IEnumerable<ErrorMessage> Errors { get; set; }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>
            {
                Success = true,
                Value = value
            };
        }

        public static Result<T> Error<T>(string message)
        {
            return Error<T>(string.Empty, message);
        }

        public static Result<T> Error<T>(string propertyName, string message)
        {
            return new Result<T>
            {
                Success = false,
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        ProprtyName = propertyName,
                        Message = message
                    }
                }
            };
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }

    public class ErrorMessage
    {
        public string ProprtyName { get; set; }
        public string Message { get; set; }
    }
}
