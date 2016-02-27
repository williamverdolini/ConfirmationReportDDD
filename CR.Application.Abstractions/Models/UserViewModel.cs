using System.Collections.Generic;

namespace CR.Application.Abstractions.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<string> Roles { get; protected set; }
    }
}
