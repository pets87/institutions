using System.ComponentModel.DataAnnotations;

namespace InstitutionsService.Validators.Translation
{
    public class TranslationUpdateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var translation = value as Models.Translation;
            if (translation == null)
            {
                ErrorMessage = "Translation object cannot be null.";
                return false;
            }
            if (!(translation.Id > 0))
            {
                ErrorMessage = "Translation object must have Id.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(translation.Language))
            {
                ErrorMessage = "Invalid Language. Language is mandatory.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(translation.Text))
            {
                ErrorMessage = "Invalid Text. Text is mandatory and cannot be empty string.";
                return false;
            }
           
            return true;
        }
    }
}
