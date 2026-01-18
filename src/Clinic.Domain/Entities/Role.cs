using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        //Navigation
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
