using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCore.Model
{
    public class User : Entity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string PassWord { get; set; }
    }
}
