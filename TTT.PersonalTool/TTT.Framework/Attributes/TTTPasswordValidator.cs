using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TTT.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TTTPasswordValidator : ValidationAttribute
    {
        public string Display { get; set; } = string.Empty;
        public bool Requied { get; set; } = false;
        public int MaximumSize { get; set; } = 1;
        public int MinimumSize { get; set; } = 1;
        public string StringRegex { get; set; } = string.Empty;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Display)) Display = validationContext.DisplayName;
            if (Requied)
            {
                if (value == null) return new ValidationResult($"{Display} is requied", new[] { validationContext.MemberName });
            }
            string strValue = $"{value}".Trim();

            if (strValue.Length > MaximumSize) return new ValidationResult($"Password must be less than {MaximumSize} charaters", new[] { validationContext.MemberName });
            if (strValue.Length < MinimumSize) return new ValidationResult($"Password must be greater than {MinimumSize} charaters", new[] { validationContext.MemberName });

            if (!string.IsNullOrEmpty(StringRegex))
            {
                Regex validateGuidRegex = new Regex(StringRegex);
                if (!validateGuidRegex.IsMatch(strValue)) return new ValidationResult($"Password format is not correct", new[] { validationContext.MemberName });
            }

            return null;
        }
    }
}
