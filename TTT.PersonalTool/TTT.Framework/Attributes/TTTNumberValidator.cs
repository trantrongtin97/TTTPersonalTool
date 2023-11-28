using System.ComponentModel.DataAnnotations;
using TTT.Framework.Utils;

namespace TTT.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TTTNumberValidator : ValidationAttribute
    {
        public string Display { get; set; } = string.Empty;
        public bool Requied { get; set; } = false;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {

            if (string.IsNullOrEmpty(Display)) Display = validationContext.DisplayName;
            if (Requied)
            {
                if (value == null) return new ValidationResult($"{Display} is requied", new[] { validationContext.MemberName });
            }
            if (value.IsNumber())
            {
                if(!value.ValidateNumberic()) return new ValidationResult($"{Display} invalid (out of range data)", new[] { validationContext.MemberName });
            }
            else
            {
                return new ValidationResult($"{Display} is a numberic field", new[] { validationContext.MemberName });
            }

            return null;
        }
    }
}
