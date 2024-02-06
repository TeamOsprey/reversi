namespace Reversi.Logic.Lobbies
{
    public class Result<T> where T : class
    {
        public bool Success { get; }
        public string Error { get; }
        public T Value { get; }

        private Result(bool success, string error, T value)
        {
            Success = success;
            Error = error;
            Value = value;
        }

        public static Result<T> CreateSuccessfulResult(T value)
        {
            return new Result<T>(true, "", value);
        }
        public static Result<T> CreateFailedResult(string error)
        {
            return new Result<T>(false, error, null);
        }
    }
}
