using EnvanterApp.Domain.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Domain.Entities.Identity
{
    public class Employee : AppUser
    {
        public List<Computer> Computers { get; set; } = new List<Computer>();
        public List<Display> Displays { get; set; } = new List<Display>();
        public List<Keyboard> Keyboards { get; set; } = new List<Keyboard>();
        public List<Mouse> Mouses { get; set; } = new List<Mouse>();
    }
}
