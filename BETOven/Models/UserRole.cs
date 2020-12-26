using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETOven.Models
{
    public class UserRole
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public List<string> Roles { get; set; }
    }
}