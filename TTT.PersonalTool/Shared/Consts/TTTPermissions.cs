namespace TTT.PersonalTool.Shared.Const;
public static class TTTPermissions
{
    public const string Root = "TTT";

    public const string Tenant = "MT";
    public const string GroupName = $"{Root}.{Tenant}";

    public const string Admin = $"{GroupName}.Admin";
    public const string Member = $"{GroupName}.Member";
}
