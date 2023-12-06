using System.ComponentModel.DataAnnotations;

namespace TTT.Framework.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class DataLookUpAttribute : Attribute
{
    public DataLookUpAttribute(string targetProperty)
    {
        TargetProperty = targetProperty;
    }

    public string TargetProperty { get; set; }

}
