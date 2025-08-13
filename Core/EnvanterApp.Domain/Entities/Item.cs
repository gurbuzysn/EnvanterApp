using EnvanterApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Domain.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; } = null!;
        public Categories Category { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string SerialNumber { get; set; } = null!;

        public Assignment Assignment { get; set; }
    }
}
