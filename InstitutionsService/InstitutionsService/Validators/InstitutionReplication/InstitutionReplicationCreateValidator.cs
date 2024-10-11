using InstitutionsService.Models;
using System.ComponentModel.DataAnnotations;

namespace InstitutionsService.Validators.InstitutionReplication
{
    public class InstitutionReplicationCreateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var institutionReplications = value as List<Models.InstitutionReplication>;

            if (institutionReplications == null) 
            {
                ErrorMessage = "InstitutionReplication List of objects cannot be null.";
                return false;
            }

            int i = 0;
            foreach (var institutionReplication in institutionReplications) 
            {
                var elementAtString = $"Element at position {i++}: ";
                if (institutionReplication == null)
                {
                    ErrorMessage = elementAtString + "InstitutionReplication object cannot be null.";
                    return false;
                }

                if (institutionReplication.Id > 0)
                {
                    ErrorMessage = elementAtString + "Invalid Id. Id must null or 0.";
                    return false;
                }

                if (!(institutionReplication.InstitutionId > 0))
                {
                    ErrorMessage = elementAtString + "Invalid InstitutionId. InstitutionId is mandatory and must be greater than 0.";
                    return false;
                }

                if (!(institutionReplication.EnvironmentClassifierId > 0))
                {
                    ErrorMessage = elementAtString + "Invalid EnvironmentClassifierId. EnvironmentClassifierId is mandatory and must be greater than 0.";
                    return false;
                }

                if (!(institutionReplication.SystemClassifierId > 0))
                {
                    ErrorMessage = elementAtString + "Invalid SystemClassifierId. SystemClassifierId is mandatory and must be greater than 0.";
                    return false;
                }
            }

            return true;
        }      
    }
}
