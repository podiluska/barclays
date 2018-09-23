namespace FileData
{
    public class MethodResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Result { get; set; }

        public string ErrorMessage { get; set; }

        public MethodResult()
        {
        }

        public MethodResult(T result)
        {
            IsSuccess = true;
            Result = result;
        }

        public static MethodResult<T> Fail(string errorMessage)
        {
            return new MethodResult<T>()
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
            };
        }
    }
}
