using System.ComponentModel.DataAnnotations;

namespace TTT.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TTTStringValidator : ValidationAttribute
    {
        public string Display { get; set; } = string.Empty;
        public bool Requied { get; set; } = false;
        public int MaximumSize { get; set; } = 1;
        public int MinimumSize { get; set; } = 0;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Display)) Display = validationContext.DisplayName;
            if (Requied)
            {
                if (value == null) return new ValidationResult($"{Display} is requied", new[] { validationContext.MemberName });
            }
            string strValue = $"{value}".Trim();

            if (strValue.Length > MaximumSize) return new ValidationResult($"{Display} must be less than {MaximumSize} charaters", new[] { validationContext.MemberName });
            if (strValue.Length < MinimumSize) return new ValidationResult($"{Display} must be greater than {MinimumSize} charaters", new[] { validationContext.MemberName });

            return null;
        }
    }
}
