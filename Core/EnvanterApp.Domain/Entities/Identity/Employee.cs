using EnvanterApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EnvanterApp.Domain.Entities.Identity
{
    public class Employee : IdentityUser<Guid>
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Department Department { get; set; }
        public DateTime HireDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUri { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

    }
}
