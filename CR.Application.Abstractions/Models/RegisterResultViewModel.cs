using System.Collections.Generic;

namespace CR.Application.Abstractions.Models
{
    public class RegisterResultViewModel
    {
        public bool Succeeded { get; set; }
        public IList<string> Errors { get; set; }
    }
}
