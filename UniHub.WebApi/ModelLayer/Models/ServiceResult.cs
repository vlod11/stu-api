namespace UniHub.WebApi.ModelLayer.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public EOperationResult Code { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Result { get; private set; }

        public static ServiceResult<T> Ok(T result) => new ServiceResult<T>
        {
            IsSuccess = true,
            Code = EOperationResult.Ok,
            Result = result,
            ErrorMessage = null
        };

        public static ServiceResult<T> Fail
            (EOperationResult resultCode) =>
            new ServiceResult<T>
            {
                IsSuccess = false,
                Code = resultCode,
                Result = default(T),
                ErrorMessage = null
            };

        public static ServiceResult<T> Fail
            (EOperationResult resultCode, string errorMessage) =>
            new ServiceResult<T>
            {
                IsSuccess = false,
                Code = resultCode,
                Result = default(T),
                ErrorMessage = errorMessage
            };

        public static ServiceResult<T> Ok() => Ok(default(T));
    }
}