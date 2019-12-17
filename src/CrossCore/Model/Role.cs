using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCore.Model
{
    public class Role : Entity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
