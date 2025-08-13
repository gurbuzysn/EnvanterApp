using EnvanterApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime AssignmentDate { get; set; }

        public Employee Employee { get; set; }
        public Item Item { get; set; }

    }
}
