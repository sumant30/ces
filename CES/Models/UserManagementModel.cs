using CES.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CES.Api.Models
{
    public class UserManagementModel:IValidatableObject
    {
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string GrantType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantType != GrantTypes.Sent.ToString())
            {
                yield return new ValidationResult("A grant type of sent has to be supplied in body");
            }
        }
    }
}
