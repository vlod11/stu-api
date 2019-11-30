namespace UniHub.Model.Models
{
    public enum EOperationResult
    {
        Ok = 0,
        SendEmailError = 1,
        ValidationError = 2,
        EntityNotFound = 3,
        AlreadyExist = 4,
        NotEnoughUniCoins = 5
    }
}