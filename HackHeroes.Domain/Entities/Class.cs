using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Domain.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? EncodedName { get; set; } = default!;
        public List<Student> Students { get; set; } = new();

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
        

    }
}
