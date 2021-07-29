using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Model
{
    public class Device
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public Guid? RoomId { get; set; }
        public string Status { get; set; }
        public Type? Type { get; set; } //from enum 
    }
}
