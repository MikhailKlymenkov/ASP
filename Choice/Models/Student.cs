using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Choice.Models
{
    public class Student : IdentityUser
    {
        public string Group { set; get; }
        public List<Discipline> Disciplines { set; get; }
    }
    
}
