namespace TTT.PersonalTool.Shared.Const;
public static class TTTPermissions
{
    public const string Root = "TTT";

    public const string TTTAdmin = $"{Root}.TTTAdmin";

    public const string Admin = $"{Root}.Admin";
    public const string Manager = $"{Root}.Manager";
    public const string Employee = $"{Root}.Employee";
    public const string Member = $"{Root}.Member";

    public static string[] Policy_LvFull = new string[] { TTTAdmin, Admin, Manager, Employee,Member };
    public static string[] Policy_LvEmployee = new string[] { TTTAdmin, Admin, Manager, Employee };
    public static string[] Policy_LvManager = new string[] { TTTAdmin, Admin, Manager };
    public static string[] Policy_LvAdmin = new string[] { TTTAdmin, Admin };
}
