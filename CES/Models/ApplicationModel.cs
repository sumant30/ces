using System.ComponentModel.DataAnnotations;

namespace CES.Api.Models
{
    public class ApplicationModel
    {
        [Required]
        public string ApplicationName { get; set; }
    }
}
