using CES.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CES.Api.Models
{
    public class ApplicationRequestModel:IValidatableObject
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public Guid AppId { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string GrantType { get; set; }
        [Required]
        public string AccessType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(GrantType!= GrantTypes.Sent.ToString().ToLower()) 
            {
                yield return new ValidationResult("A grant type of sent has to be supplied in body");
            }
        }
    }
}
