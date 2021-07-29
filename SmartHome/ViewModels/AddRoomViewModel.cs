using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.ViewModels
{
    public class AddRoomViewModel
    {
        [Required(AllowEmptyStrings = false), MaxLength(255)] //from System.ComponentModel.DataAnnotations
        public string Name { get; set; }
    }
}
