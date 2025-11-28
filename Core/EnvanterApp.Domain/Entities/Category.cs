using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
    }
}
