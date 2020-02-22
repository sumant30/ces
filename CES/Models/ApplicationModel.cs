using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CES.Api.Models
{
    public class ApplicationModel
    {
        [Required]
        public string ApplicationName { get; set; }
    }
}
