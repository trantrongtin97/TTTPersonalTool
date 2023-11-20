namespace TTT.PersonalTool.Server.Services.IServices;

public interface ITTTSercurity
{
    public string EncryptAES(string rawStr);
    public string DecryptAES(string encryptStr);
}
