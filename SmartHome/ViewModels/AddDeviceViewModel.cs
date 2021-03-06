using SmartHome.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.ViewModels
{
    public class AddDeviceViewModel
    {
        [Required(AllowEmptyStrings = false), MaxLength(255)]
        public string Name { get; set; }
        public Guid? RoomId { get; set; }
        [MaxLength(1000)]
        public string Status { get; set; }
        public DeviceType? DeviceType { get; set; } //from enum 
    }
}
