namespace UniHub.WebApi.BLL.Helpers.Contract
{
    public interface IEncryptHelper
    {
        string Encrypt<T>(T obj);
        string Encrypt(string plaintext);
        bool TryDecrypt<T>(string encryptedText, out T obj);
        bool TryDecrypt(string encryptedText, out string decryptedText);
    }
}