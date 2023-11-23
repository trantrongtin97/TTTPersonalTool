namespace TTT.Framework.Sercurity;

public interface ITTTSercurity
{
    public string EncryptAES(string rawStr);
    public string DecryptAES(string encryptStr);
}
