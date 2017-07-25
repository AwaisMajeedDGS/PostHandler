namespace PostHandler.Foundation.Helper
{
    public sealed class ResponseMessages
    {
        public const string NoUserFound = "No User Found";
        public const string UserAlreadyExist = "User already exist";
        public const string UserAlreadyExistForEmail = "User already exist for the given Email";
        public const string UserAlreadyExistForId = "User already exist for the given ID";

        public const string NotFound = "Not Found";
        public const string BadRequest = "Bad Request";
        public const string InternalServerError = "There was an exception";
    }
}
