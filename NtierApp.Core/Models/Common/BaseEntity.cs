using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierApp.Core.Models.Common
{
    public class BaseEntity : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
    }

    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
