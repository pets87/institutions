using System.ComponentModel.DataAnnotations;

namespace InstitutionsService.Validators.Institution
{
    public class InstitutionUpdateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var institution = value as Models.Institution;
            if (institution == null)
            { 
                ErrorMessage = "Institution object cannot be null.";
                return false;
            }
            if (!(institution.Id > 0))
            {
                ErrorMessage = "Institution object must have Id.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(institution.Name))
            {
                ErrorMessage = "Invalid name. Name is mandatory.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(institution.RegCode))
            {
                ErrorMessage = "Invalid RegCode. RegCode is mandatory.";
                return false;
            }
            if (!(institution.TypeClassifierId > 0))
            {
                ErrorMessage = "Invalid TypeClassifierId. TypeClassifierId is mandatory and must be greater that 0.";
                return false;
            }
            if (!(institution.AddressId > 0))
            {
                ErrorMessage = "Invalid AddressId. AddressId is mandatory and must be greater that 0.";
                return false;
            }
            if (institution.ValidFrom == null|| institution.ValidFrom == DateTime.MinValue)
            {
                ErrorMessage = "Invalid ValidFrom. ValidFrom is mandatory.";
                return false;
            }
            if (institution.ValidFrom != null && institution.ValidTo != null && institution.ValidFrom > institution.ValidTo) 
            {
                ErrorMessage = "ValidFrom cannot be later than Validto.";
                return false;
            }
            return true;
        }
    }
}
