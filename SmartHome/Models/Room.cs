using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Model
{
    public class Room
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
    }
}
