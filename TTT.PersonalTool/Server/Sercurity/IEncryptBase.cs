namespace TTT.PersonalTool.Server.Sercurity;
public interface IEncryptBase
{
    public abstract string EncryptPassword(string password);
    public abstract string DecriptPassword(string encriptPass);
}