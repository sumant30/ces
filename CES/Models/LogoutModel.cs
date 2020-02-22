using CES.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CES.Api.Models
{
    public class LogoutModel:IValidatableObject
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string GrantType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantType?.ToLower() != GrantTypes.Sent.ToString().ToLower())
            {
                yield return new ValidationResult($"A grant type of { GrantTypes.Sent.ToString() } has to be supplied in body");
            }
        }
    }
}
