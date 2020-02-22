using CES.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CES.Api.Models
{
    public class AccessTypeModel : IValidatableObject
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ApplicationId { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string GrantType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantType?.ToLower() != GrantTypes.Sent.ToString().ToLower())
            {
                yield return new ValidationResult("A grant type of sent has to be supplied in body");
            }
        }
    }
}
